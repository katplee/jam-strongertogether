using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData instance;
    public static SaveData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SaveData();
            }
            return instance;
        }
    }
}
