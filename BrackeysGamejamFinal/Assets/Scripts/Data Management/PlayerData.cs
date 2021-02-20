using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private int count = 20;

    private Player player;

    //player stats
    public Element.ElementType playerType;
    public float playerHP;
    public float playerArmor;
    public float playerDamageAmount;
    public Element.WeaknessType playerWeakness;
    public float playerWeaknessFactor;
    public int playerFireAttack;
    public int playerWaterAttack;
    public int playerWindAttack;
    public int playerEarthAttack;

    void Start()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SavePlayer();
    }

    private void SavePlayer()
    {
        player = FindObjectOfType<Player>();
                
        playerType = player.Type;
        playerHP = player.hp;
        playerArmor = player.armor;
        playerDamageAmount = player.DamageAmount();
        playerWeakness = player.weakness;
        playerWeaknessFactor = player.weaknessFactor;

        playerFireAttack = player.fireAttack;
        playerWaterAttack = player.waterAttack;
        playerWindAttack = player.windAttack;
        playerEarthAttack = player.earthAttack;
        
    }
}
