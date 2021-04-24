using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Object = System.Object;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class FightManager : MonoBehaviour
{
    public static event Action OnFightEnd;

    private static FightManager instance;
    public static FightManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FightManager();
            }
            return instance;
        }
    }

    //GENERAL PARAMETERS
    private BattleState state;
    public float timeToWait = 2f; //remove this eventually

    //ACTIONS PANEL UI
    public TMP_Text dialogueBox;
    public Button attackButton;
    public Button leaveButton;
    public Button switchButton;
    
    //DRAGON PANEL UI
    public GameObject dragonPanel;

    //PLAYER CORNER UI
    public Player Player { get; set; }
    public GameObject PGO { get; set; }
    public UIBattleHUD PHUD { get; set; }
    
    //ENEMY CORNER UI
    public Enemy Enemy { get; set; }
    public GameObject EGO { get; set; }
    public UIBattleHUD EHUD { get; set; }


    public Dragon currentDragon;
    public int currentDragonIndex = 0;


    [Header("Transition Values")]
    public SceneTransition sceneTransition;

    void Start()
    {
        //state = BattleState.START;
        //OnStateChange?.Invoke(state);
        //Debug.Log(Player);
    }

    private void SetupBattle()
    {
        //playerGO = Instantiate(playerPrefab, playerCorner);
        //player = playerGO.AddComponent<Player>();
        //playerHUD.UpdateHUD(player);

        //enemyGO = Instantiate(enemyPrefab, enemyCorner);
        //enemy = enemyGO.GetComponent<Enemy>();
        //enemyHUD.UpdateHUD<Element>(enemy);

        

        state = BattleState.PLAYERTURN;
        OnPlayerTurn();
    }

    private void SetEnemySprite()
    {
        //throw new NotImplementedException();
    }

    private void SetEnemyStats()
    {
        /*
        LastEnemyData saved = LastEnemyData.Instance;

        enemyGO.GetComponent<SpriteRenderer>().sprite = saved.enemySprite;
               
        enemy.hp = saved.enemyHP;
        enemy.armor = saved.enemyArmor;
        enemy.damageAmount = saved.enemyDamageAmount;

        enemy.weakness = saved.enemyWeakness;
        enemy.weaknessFactor = saved.enemyWeaknessFactor;

        enemy.fireAttack = saved.enemyFireAttack;
        enemy.waterAttack = saved.enemyWaterAttack;
        enemy.windAttack = saved.enemyWindAttack;
        enemy.earthAttack = saved.enemyEarthAttack;
        */
    }

    private void SetPlayerStats()
    {
        /*
        if (player.Type == Element.ElementType.PLAYER)
        {
            PlayerData saved = PlayerData.Instance;

            player.hp = saved.playerHP;
            player.armor = saved.playerArmor;
            player.damageAmount = saved.playerDamageAmount;

            player.weakness = saved.playerWeakness;
            player.weaknessFactor = saved.playerWeaknessFactor;

            player.fireAttack = saved.playerFireAttack;
            player.waterAttack = saved.playerWaterAttack;
            player.windAttack = saved.playerWindAttack;
            player.earthAttack = saved.playerEarthAttack;
        }
        */
    }

    private void UpdateSavedPlayerStats()
    {
        /*
        Debug.Log("It's a human!");
        PlayerData saved = PlayerData.Instance;

        saved.playerHP = player.hp;
        saved.playerArmor = player.armor;
        saved.playerDamageAmount = player.damageAmount;

        saved.playerWeakness = player.weakness;
        saved.playerWeaknessFactor = player.weaknessFactor;

        saved.playerFireAttack = player.fireAttack;
        saved.playerWaterAttack = player.waterAttack;
        saved.playerWindAttack = player.windAttack;
        saved.playerEarthAttack = player.earthAttack;
        */
    }

    private void UpdateSavedDragonStats()
    {
        ///List<Object> saved = DragonsData.sortedDragonsStats[currentDragonIndex - 1];
        ///         dragon type,
        ///         hp,
        ///         armor,
        ///         damage amount,
        ///         weakness,
        ///         weakness factor,
        ///         fire attack,
        ///         water attack,
        ///         wind attack,
        ///         earth attack

        //saved[1] = player.armor;        
    }

    private void OnPlayerTurn()
    {
        dialogueBox.text = state.ToString();

        //player can attack
        attackButton.interactable = true;

        //player can leave
        leaveButton.interactable = true;
    }

    IEnumerator OnEnemyTurn()
    {
        dialogueBox.text = state.ToString();

        //player cannot attack
        attackButton.interactable = false;

        //player cannot leave
        leaveButton.interactable = false;

        yield return new WaitForSeconds(timeToWait);

        //bool playerIsDead = player.TakeDamage(enemy.DamageAmount());

        //update the saved stats
        //UpdateSavedPlayerStats();
        if (currentDragonIndex != 0)
        {
            UpdateSavedDragonStats();
        }
        OnFightEnd?.Invoke();

        //update the player stats
        //playerHUD.UpdateHPArmor<Element>(player.hp, player.armor, player.maxHP, player.maxArmor);

        //update the dialogue
        dialogueBox.text = "ENEMYATTACKDONE";

        yield return new WaitForSeconds(timeToWait);

        //CheckForEnemyWin(playerIsDead);
    }

    private void DealDamage()
    {
        //if (enemy.Type == ElementType.DRAGON)
        //{

        //}

        //bool playerIsDead = player.TakeDamage(enemy.DamageAmount());
        //return playerIsDead;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) { return; }

        Debug.Log("Attack!");

        StartCoroutine(DealAttack());
    }

    public void TESTOnEnemyDefeated()
    {
        EnemySave.Instance.lastEnemy.hp = 0;
        EnemySave.Instance.lastEnemy.armor = 0;
    }

    IEnumerator DealAttack()
    {
        //bool enemyIsDead = enemy.TakeDamage(player.DamageAmount());

        //update the enemy stats
        //enemyHUD.UpdateHPArmor<Element>(enemy.hp, enemy.armor, enemy.maxHP, enemy.maxArmor);

        //update the dialogue
        dialogueBox.text = "PLAYERATTACKDONE";

        yield return new WaitForSeconds(timeToWait);

        //CheckForPlayerWin(enemyIsDead);
    }

    public void OnSwitchButton()
    {
        if (state != BattleState.PLAYERTURN) { return; }

        //Show a panel where you can choose your dragons
        if (dragonPanel.activeSelf) { dragonPanel.SetActive(false); }
        else { dragonPanel.SetActive(true); }
    }

    public void OnSwitchDragon(GameObject dragonGO)
    {
        if (state != BattleState.PLAYERTURN) { return; }

        dragonGO.TryGetComponent<PanelLabel>(out PanelLabel dragonLabel);

        //if the dragon is still the same, return
        if (currentDragonIndex == dragonLabel.index) { return; }

        //save the current armor to the hp of the old dragon
        if (currentDragonIndex != 0)
        {
            //currentDragon.hp = player.armor;
            UpdateSavedDragonStats();
            OnFightEnd?.Invoke();
        }        

        //if the dragon is new, do the new fusion
        currentDragonIndex = dragonLabel.index;

        //change the player script to the chosen dragon
        dragonGO.TryGetComponent<Dragon>(out Dragon dragonChosen);

        ///add the dragon's hp to your armor
        currentDragon = dragonChosen;
        //player.armor = dragonChosen.hp;
        //player.maxArmor = dragonChosen.maxHP;

        //change the sprite to the chosen dragon
        //playerGO.GetComponent<SpriteRenderer>().sprite = ChoosePlayerSprite(dragonChosen.DType);

        //update the HUD
        //playerHUD.UpdateHUD(player);

        StartCoroutine(SwitchDragon());
    }

    private void ChoosePlayerSprite(DragonType dType)
    {
        /*
        switch (dType)
        {
            case DragonType.FIRE:
                return fireFusedSprite;

            case DragonType.WATER:
                return waterFusedSprite;

            case DragonType.WIND:
                return windFusedSprite;

            case DragonType.EARTH:
                return earthFusedSprite;

            case DragonType.BASE:
                return baseFusedSprite;
        }
        */

        //return playerSprite;
    }

    IEnumerator SwitchDragon()
    {
        yield return new WaitForSeconds(timeToWait);

        state = BattleState.ENEMYTURN;
        StartCoroutine(OnEnemyTurn());
    }

    private void CheckForPlayerWin(bool enemyIsDead)
    {
        if (enemyIsDead)
        {
            state = BattleState.WON;
            OnEndFight();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(OnEnemyTurn());
        }
    }

    private void CheckForEnemyWin(bool playerIsDead)
    {
        if (playerIsDead)
        {
            state = BattleState.LOST;
            OnEndFight();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            OnPlayerTurn();
        }
    }

    public void OnEndFight()
    {
        attackButton.interactable = false;

        if (state == BattleState.WON)
        {
            dialogueBox.text = "PLAYERWON";
            LastEnemyData.enemyDefeated = true;
        }
        else if (state == BattleState.LOST)
        {
            dialogueBox.text = "PLAYERLOST";
            //disable the battle scene/something
        }
    }    

    public void OnLeaveButton()
    {
        RestartDragonStats();
        OnFightEnd?.Invoke();
        sceneTransition.FadeTo(LevelData.mapScene);
    }

    private void RestartDragonStats()
    {
        List<List<Object>> saved = DragonsData.sortedDragonsStats;

        foreach (List<Object> dragonList in saved)
        {
            dragonList[1] = dragonList[10];
        }
    }
}