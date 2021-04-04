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

        foreach (EnemyData _enemyData in enemySave.enemies)
        {
            if (name == _enemyData.name)
            {
                enemyData = _enemyData;

                //BASIC STATS
                hp = enemyData.hp;
                maxHP = enemyData.maxHP;
                DType = enemyData.dType;
                name = enemyData.name;

                //COMBAT STATS
                Armor = enemyData.armor;
                maxArmor = enemyData.maxArmor;
                Weakness = enemyData.weakness;
                weaknessFactor = enemyData.weaknessFactor;
                fireAttack = enemyData.fireAttack;
                waterAttack = enemyData.waterAttack;
                windAttack = enemyData.windAttack;
                earthAttack = enemyData.earthAttack;
                baseAttack = enemyData.baseAttack;

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

    /*
     * POPULATE WITH ENEMY:
     * This method adds the enemy data to the List<EnemyData> enemies list in EnemySave.
     * Before calling this method, make sure that the enemy data in this class is the version you want to add.
     * Populating the list does not mean replacing elements in the list; therefore, calling this will ADD more elements to the enemies list.
     * In case you wish to REPLACE elements in the enemies list, call the ResaveThisEnemy method instead.
     */
    public void PopulateWithEnemy()
    {
        InitializeSerialization();
        EnemySave.Instance.PopulateEnemyList(enemyData);
        EnemySave.Instance.SaveEnemyData();
    }

    private void ResaveLastEnemy()
    {
        EnemySave.Instance.ReplaceLastEnemyList();
        EnemySave.Instance.SaveEnemyData();
    }

    /*
     * RESAVE THIS ENEMY:
     * This method updates the enemy data that is included in the array.
     * Before calling this method, make sure that the enemy data in this class is the updated one.
     * Also, make sure that the data of the enemy whose data you wish to add is ALREADY in the list.
     * Calling this method without the enemy already added will throw an exception and crash the game.
     */
    private void ResaveThisEnemy()
    {
        InitializeSerialization();
        EnemySave.Instance.ReplaceEnemyList(enemyData);
        EnemySave.Instance.SaveEnemyData();
    }

    /*
     * IS LAST ENEMY:
     * This method checks, upon returning to the basic scene, if the enemy which holds this script was the enemy that was in battle with the player.
     * Note that the game object's name is used in checking.
     */
    private bool IsLastEnemy()
    {
        EnemySave enemySave = EnemySave.Instance.LoadEnemyData();

        if (enemySave.lastEnemy.name == null) { return false; }

        return (enemySave.lastEnemy.name == gameObject.name) ?
            true : false;
    }    

    /*
     * RELOAD THIS ENEMY:
     * This method downloads this enemy's corresponding data from the List<EnemyData> enemies list.
     * In the event that this enemy was defeated (hp + armor = 0), this method also destroys the game object which holds this script,
     *    and the data from the List<EnemyData> enemies list.
     */
    public void ReloadThisEnemy()
    {
        if (IsLastEnemy())
        {
            Debug.Log("Is last enemy!");
            ResaveLastEnemy();
            Debug.Log("Updated last enemy!");
        }
        
        InitializeDeserialization();        

        float life = hp + Armor;
        if (life == 0)
        {
            EnemySave.Instance.RemoveEnemyList(enemyData);
            EnemySave.Instance.SaveEnemyData();
            OnDestroy();
            Destroy(gameObject);
        }
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
