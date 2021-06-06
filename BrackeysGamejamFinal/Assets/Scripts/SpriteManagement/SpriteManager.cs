using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class SpriteManager : MonoBehaviour
{
    public event Action Release;

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
    
    private int refIndex;
    private AssetReference spriteSheet;
    private Queue<Sprite> animSprites;
    private int animKeyFrameCount = 4;
    private int animAvailableKeyFrames = 3;
    //private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> _asyncOperationHandles =
    //    new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();
    private EnemySpriteLoader enemySpriteLoader;

    #endregion

    private void LoadAndAssign(AssetReference assetReference)
    {
        Addressables.LoadAssetAsync<IList<Sprite>>(spriteSheet).Completed += (obj) =>
        {
            if (obj.Result == null)
            {
                Debug.LogError("Sheets not uploaded properly.");
                return;
            }

            for (int i = 0; i < animKeyFrameCount; i++)
            {
                if (i == animAvailableKeyFrames) { i = 0; }

                animSprites.Enqueue(obj.Result[refIndex * 3 + i]);

                if (animSprites.Count == animKeyFrameCount) { break; }
            }
        };

        AssignSprites();
    }

    private void Remove()
    {
        Addressables.ReleaseInstance(obj.gameObject);

        
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
        for (int i = 0; i < animKeyFrameCount; i++)
        {
            enemySpriteLoader.Sprites[i] = animSprites.Dequeue();
        }
    }



}
