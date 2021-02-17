using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This script will only be activated in the event that a battle will ensue.
/// </summary>

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class FightManager : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerCorner;
    public Transform enemyCorner;

    //fix this part. make it more general
    private Element player;
    private Element enemy;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    private void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerCorner);
        player = playerGO.GetComponent<Dragon>();
        playerHUD.UpdateHUD(player);

        GameObject enemyGO = Instantiate(enemyPrefab, enemyCorner);
        enemy = enemyGO.GetComponent<Enemy>();
        enemyHUD.UpdateHUD(enemy);

    }
}
