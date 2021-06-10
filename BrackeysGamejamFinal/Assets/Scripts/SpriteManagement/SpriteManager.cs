using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class SpriteManager : MonoBehaviour
{
    public static event Action OnTransferComplete;

    private static SpriteManager instance;
    public static SpriteManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpriteManager>();
            }
            return instance;
        }
    }

    #region Addresses
    private const string enemiesSpriteSheetAddress = "Sprites/ENEMIES.png";
    #endregion

    #region Enemy sprite manager
    public int refIndex;
    private Queue<Sprite> animSprites = new Queue<Sprite>();
    private int animKeyFrameCount = 4;
    private int animAvailableKeyFrames = 3;
    //private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> _asyncOperationHandles =
    //    new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();
    private EnemySpriteLoader enemySpriteLoader;
    #endregion

    public void LoadAndAssign()
    {
        Addressables.LoadAssetAsync<IList<Sprite>>(enemiesSpriteSheetAddress).Completed += (obj) =>
        {
            if (obj.Result == null)
            {
                Debug.LogError("Sheets not uploaded properly.");
                return;
            }

            for (int i = 0; i < animKeyFrameCount; i++)
            {
                if (i == animAvailableKeyFrames) { i = 0; }

                animSprites.Enqueue(obj.Result[(refIndex / animAvailableKeyFrames) * animAvailableKeyFrames + i]);

                if (animSprites.Count == animKeyFrameCount) { break; }
            }

            //assign sprites for animation
            AssignSprites();
            OnTransferComplete?.Invoke();
        };
    }

    public void AssignEnemyRefIndex(int index)
    {
        refIndex = index;
    }

    public void AssignEnemySpriteLoader(EnemySpriteLoader loader)
    {
        enemySpriteLoader = loader;
    }

    private void AssignSprites()
    {
        if (enemySpriteLoader.Sprites == null) { enemySpriteLoader.GenerateList(); }

        for (int i = 0; i < animKeyFrameCount; i++)
        {
            enemySpriteLoader.Sprites.Add(animSprites.Dequeue());
        }
    }
}
