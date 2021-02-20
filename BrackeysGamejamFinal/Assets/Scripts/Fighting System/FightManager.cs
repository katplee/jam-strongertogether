using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This script will only be activated in the event that a battle will ensue.
/// Introduce a system so that the fight manager starts from the start method whenever a battle ensues.
/// Make sure that the events are synced properly, called whenever it should be called.
/// </summary>

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class FightManager : MonoBehaviour
{
    //public static event Action OnStartFight;
    //public static event Action OnEndFight;

    public BattleState state;

    public Button attackButton;
    public Button leaveButton;
    public TMP_Text dialogueBox;
    public float timeToWait = 2f;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerCorner;
    public Transform enemyCorner;

    //fix this part. make it more general
    public Player player;
    private Enemy enemy;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    [Header("Transition Values")]
    public string sceneName = "BasicScene 2";
    public SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerCorner);
        player = playerGO.AddComponent<Player>();
        SetPlayerStats();
        playerHUD.UpdateHUD<Element>(player);

        GameObject enemyGO = Instantiate(enemyPrefab, enemyCorner);
        enemy = enemyGO.GetComponent<Enemy>();
        enemyHUD.UpdateHUD<Element>(enemy);

        yield return new WaitForSeconds(timeToWait);

        state = BattleState.PLAYERTURN;
        OnPlayerTurn();
    }

    private void SetPlayerStats()
    {
        PlayerData saved = PlayerData.Instance;

        player.hp = saved.playerHP;
        player.armor = saved.playerArmor;
        player.damageAmount = saved.playerDamageAmount;
        player.fireAttack = saved.playerFireAttack;
        player.waterAttack = saved.playerWaterAttack;
        player.windAttack = saved.playerWindAttack;
        player.earthAttack = saved.playerEarthAttack;
    }

    private void UpdateSavedStats()
    {
        PlayerData saved = PlayerData.Instance;

        saved.playerHP = player.hp;



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
        UpdateSavedStats();

        //update the player stats
        playerHUD.UpdateHP<Element>(player.hp);

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
        enemyHUD.UpdateHP<Element>(enemy.hp);

        //update the dialogue
        dialogueBox.text = "PLAYERATTACKDONE";

        yield return new WaitForSeconds(timeToWait);

        CheckForPlayerWin(enemyIsDead);
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
        }
        else if (state == BattleState.LOST)
        {
            dialogueBox.text = "PLAYERLOST";
            //disable the battle scene/something
        }
    }

    public void OnLeaveButton()
    {
        sceneTransition.FadeTo(sceneName);
    }
}