using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager instance;
    public static PrefabManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PrefabManager>();
            }
            return instance;
        }
    }

    public GameObject PPlayer;
    public GameObject PEnemy;
}
