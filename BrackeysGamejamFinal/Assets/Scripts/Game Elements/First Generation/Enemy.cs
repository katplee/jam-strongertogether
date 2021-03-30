using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Things to decide on:
 * *to make the fight scene pace faster, should the hp of the enemy be dependent on the level?
 *      check temporary fix to this.
 */

public class Enemy : Element
{
    private EnemyData enemyData;

    public override ElementType Type
    {
        get { return ElementType.ENEMY; }
    }

    protected override void Awake()
    {
        base.Awake();
        SubscribeEvents();
    }

    private void Update()
    {
        //TESTPrintEnemyData();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnsubscribeEvents();
    }

    protected override void InitializeAttacks()
    {
        int currentLvl = GameManager.currLvl;
        int attack = Random.Range(1, currentLvl * attackMargin + 1);

        baseAttack = ((currentLvl == GameManager.baseLevel) ? specialtyAttackMultiplier : 0) * attack;
        fireAttack = ((currentLvl == GameManager.fireLevel) ? specialtyAttackMultiplier : 0) * attack;
        waterAttack = ((currentLvl == GameManager.waterLevel) ? specialtyAttackMultiplier : 0) * attack;
        windAttack = ((currentLvl == GameManager.windLevel) ? specialtyAttackMultiplier : 0) * attack;
        earthAttack = ((currentLvl == GameManager.earthLevel) ? specialtyAttackMultiplier : 0) * attack;
    }

    public override void InitialSerialization()
    {
        //add enemy to level enemy list
        PopulateWithEnemy();
    }

    protected override void InitializeSerialization()
    {
        enemyData = new EnemyData();

        //BASIC STATS
        enemyData.hp = hp;
        enemyData.maxHP = maxHP;
        enemyData.type = Type;
        enemyData.dType = DType;
        enemyData.id = GetInstanceID();
        enemyData.name = name;

        //COMBAT STATS
        enemyData.armor = Armor;
        enemyData.maxArmor = maxArmor;
        enemyData.weakness = Weakness;
        enemyData.weaknessFactor = weaknessFactor;
        enemyData.fireAttack = fireAttack;
        enemyData.waterAttack = waterAttack;
        enemyData.windAttack = windAttack;
        enemyData.earthAttack = earthAttack;
        enemyData.baseAttack = baseAttack;
    }

    public override void InitializeDeserialization()
    {
        EnemySave enemySave = EnemySave.Instance.LoadEnemyData();

        foreach (EnemyData enemy in enemySave.enemies)
        {
            if (enemy.name == enemyData.name)
            {
                enemyData = enemy;
                Debug.Log(enemyData.name);
            }
        }
    }

    public void AssignAsLastEnemy()
    {
        InitializeSerialization();
        EnemySave.Instance.AssignLastEnemy(enemyData);
        EnemySave.Instance.SaveEnemyData();
    }

    public void PopulateWithEnemy()
    {
        InitializeSerialization();
        EnemySave.Instance.PopulateEnemyList(enemyData);
        EnemySave.Instance.SaveEnemyData();
    }

    public void ResaveThisEnemy()
    {
        InitializeSerialization();
        EnemySave.Instance.ReplaceEnemyList(enemyData);
        EnemySave.Instance.SaveEnemyData();
    }

    public void ReloadThisEnemy()
    {
        InitializeDeserialization();

        /*
        float life = enemyData.hp + enemyData.armor;
        if (life == 0)
        {
            EnemySave.Instance.RemoveEnemyList(enemyData);
            Destroy(gameObject);
        } 
        */
    }

    private void SubscribeEvents()
    {
        SerializationCommander.ResaveAllEnemies += ResaveThisEnemy;
        SerializationCommander.ReloadAllEnemies += ReloadThisEnemy;

    }

    private void UnsubscribeEvents()
    {
        SerializationCommander.ResaveAllEnemies -= ResaveThisEnemy;
        SerializationCommander.ReloadAllEnemies -= ReloadThisEnemy;
    }

    private void TESTPrintEnemyData()
    {
        string enemyStats =
            $"//BASIC STATS\n" +
            $"hp : {enemyData.hp}\n" +
            $"maxHP : {enemyData.maxHP}\n" +
            $"type : {enemyData.type}\n" +
            $"dType : {enemyData.dType}\n" +
            $"id : {enemyData.id}\n" +
            $"name : {enemyData.name}\n\n" +
            $"//COMBAT STATS\n" +
            $"armor : {enemyData.armor}\n" +
            $"maxArmor : {enemyData.maxArmor}\n" +
            $"weakness : {enemyData.weakness}\n" +
            $"weaknessFactor : {enemyData.weaknessFactor}\n" +
            $"fireAttack : {enemyData.fireAttack}\n" +
            $"waterAttack : {enemyData.waterAttack}\n" +
            $"windAttack : {enemyData.windAttack}\n" +
            $"earthAttack : {enemyData.earthAttack}\n" +
            $"baseAttack : {enemyData.baseAttack}\n";

        Debug.Log(enemyStats);
    }
}
