using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LastEnemyParam
{
    private static LastEnemyParam instance;
    public static LastEnemyParam Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LastEnemyParam();
            }
            return instance;
        }
    }

    public string enemyType;
    public float enemyHP;
    public float enemyArmor;
    public string enemyWeakness;
    public int enemyFireAttack;
    public int enemyWaterAttack;
    public int enemyWindAttack;
    public int enemyEarthAttack;
}
