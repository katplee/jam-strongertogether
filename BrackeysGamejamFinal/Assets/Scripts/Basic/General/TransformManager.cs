using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    private static TransformManager instance;
    public static TransformManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TransformManager>();
            }
            return instance;
        }
    }

    public Transform TPlayer;
    public Transform TEnemy;
}
