using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySave
{
    private static EnemySave instance;
    public static EnemySave Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemySave();
            }
            return instance;
        }
    }
    public string path;
    
    public EnemyData lastEnemy;
    public List<EnemyData> enemies = new List<EnemyData>();
    
    public void AssignLastEnemy(EnemyData lastEnemy)
    {
        this.lastEnemy = lastEnemy;
    }

    public void PopulateEnemyList(EnemyData enemy)
    {
        enemies.Add(enemy);
    }

    public void ReplaceEnemyList(EnemyData enemy)
    {
        int index = Find(enemy);
        enemies[index] = enemy;
    }

    private int Find(EnemyData specificEnemy)
    {
        foreach (EnemyData enemy in enemies)
        {
            if (enemy.name == specificEnemy.name)
            {
                return enemies.IndexOf(enemy);
            }
            continue;
        }

        throw new NotFoundInListException();
    }

    public void SaveEnemyData()
    {        
        SerializationManager.Save(GameManager.currLvl.ToString(), GetType().Name, this, out path);
    }
    
}
