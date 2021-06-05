using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
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

    #region Enemy sprite manager
    public float RefIndex { get; set; }
    public List<AssetReference> enemies;
    private Queue<Sprite> spriteSequence;
    #endregion





}
