using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Object = System.Object;
using UnityEngine.SceneManagement;

/// <summary>
/// This script will only be activated in the event that a battle will ensue.
/// Introduce a system so that the fight manager starts from the start method whenever a battle ensues.
/// Make sure that the events are synced properly, called whenever it should be called.
/// </summary>

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class FightManager : MonoBehaviour
{
    //use this to update dragon stats
    public static event Action OnFightEnd;

    public BattleState state;

    public TMP_Text dialogueBox;
    public Button attackButton;
    public Button leaveButton;
    public Button switchButton;
    [SerializeField] private string lastButtonClicked;
    public string _attack = "attack";
    public string _switch = "switch";
    public string _leave = "leave";
    public GameObject dragonPanel;
    public float timeToWait = 2f;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject playerGO;
    public GameObject enemyGO;

    public Transform playerCorner;
    public Transform enemyCorner;

    //fix this part. make it more general
    public Element player;
    private Enemy enemy;

    public Dragon currentDragon;
    public int currentDragonIndex = 0;

    [SerializeField] private Sprite fireFusedSprite;
    [SerializeField] private Sprite waterFusedSprite;
    [SerializeField] private Sprite windFusedSprite;
    [SerializeField] private Sprite earthFusedSprite;
    [SerializeField] private Sprite baseFusedSprite;
    [SerializeField] private Sprite playerSprite;


    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    [Header("Transition Values")]
    public SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGO = Instantiate(playerPrefab, playerCorner);
        player = playerGO.AddComponent<Player>();
        //SetPlayerStats();
        playerHUD.UpdateHUD(player);

        enemyGO = Instantiate(enemyPrefab, enemyCorner);
        enemy = enemyGO.GetComponent<Enemy>();
        //SetEnemyStats();        
        enemyHUD.UpdateHUD<Element>(enemy);

        yield return new WaitForSeconds(timeToWait);

        state = BattleState.PLAYERTURN;
        OnPlayerTurn();
    }

    private void SetEnemySprite()
    {
        throw new NotImplementedException();
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
        List<Object> saved = DragonsData.sortedDragonsStats[currentDragonIndex - 1];
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

        bool playerIsDead = player.TakeDamage(enemy.DamageAmount());

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

        CheckForEnemyWin(playerIsDead);
    }

    private bool DealDamage()
    {
        if (enemy.Type == Element.ElementType.DRAGON)
        {

        }

        bool playerIsDead = player.TakeDamage(enemy.DamageAmount());
        return playerIsDead;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) { return; }

        Debug.Log("Attack!");

        StartCoroutine(DealAttack());
    }

    IEnumerator DealAttack()
    {
        bool enemyIsDead = enemy.TakeDamage(player.DamageAmount());

        //update the enemy stats
        //enemyHUD.UpdateHPArmor<Element>(enemy.hp, enemy.armor, enemy.maxHP, enemy.maxArmor);

        //update the dialogue
        dialogueBox.text = "PLAYERATTACKDONE";

        yield return new WaitForSeconds(timeToWait);

        CheckForPlayerWin(enemyIsDead);
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
        playerGO.GetComponent<SpriteRenderer>().sprite = ChoosePlayerSprite(dragonChosen.DType);

        //update the HUD
        playerHUD.UpdateHUD(player);

        StartCoroutine(SwitchDragon());
    }

    private Sprite ChoosePlayerSprite(Dragon.DragonType dType)
    {
        switch (dType)
        {
            case Dragon.DragonType.FIRE:
                return fireFusedSprite;

            case Dragon.DragonType.WATER:
                return waterFusedSprite;

            case Dragon.DragonType.WIND:
                return windFusedSprite;

            case Dragon.DragonType.EARTH:
                return earthFusedSprite;

            case Dragon.DragonType.BASE:
                return baseFusedSprite;
        }

        return playerSprite;
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