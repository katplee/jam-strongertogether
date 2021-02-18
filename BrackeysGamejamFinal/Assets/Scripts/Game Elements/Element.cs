using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <notes>
/// kat, 2/17/2021:
/// *confirm whether attacks could be increased
/// *fix damage amount
/// *delete unnecessary variables
/// </notes>

public class Element : MonoBehaviour
{

    public enum ElementType
    {
        GOLEM, BOSS, DRAGON, ENEMY
    }

    public enum WeaknessType
    {
        FIRE, WATER, WIND, EARTH
    }

    public ElementType type;

    public float hp;
    public float armor;

    [SerializeField] private float damageAmount; //for testing only

    //pertains to the amount of damage an element could cause
    //for example, if the fire attack is 3, the element could inflict 3 more damage points
    public int fireAttack;

    public int waterAttack;

    public int windAttack;

    public int earthAttack;

    public WeaknessType weakness;
    public float weaknessFactor = 2.0f;

    protected float maxHP = 100;

    /*
    public Element(ElementType type)
    {
        Type = type;
        InitializeCommonAttributes();
        InitializeUniqueAttributes(type);
    }
    */

    public void Start()
    {
        //Type = type;
        InitializeCommonAttributes();
        InitializeUniqueAttributes(type);
    }

    private void InitializeCommonAttributes()
    {
        //set starting hp to full (assumed full scale is 100)
        hp = maxHP;

        //set starting armor to full (assumed full scale is 100)
        armor = 100;

        //determine weakness
        //in the meantime, weakness is randomly generated
        //  but for the final version, we might want to establish a correlation
        //  between the element and its weakness
        //weakness will be set at the moment of instantiation
        int weaknessInd = 2;
        weakness = (WeaknessType)weaknessInd;
    }

    private void InitializeUniqueAttributes(ElementType type)
    {
        switch (type)
        {
            case ElementType.GOLEM:
                SetToGolem();
                break;

            case ElementType.BOSS:
                SetToBoss();
                break;

            case ElementType.DRAGON:
                SetToDragon();
                break;

            case ElementType.ENEMY:
                SetToEnemy();
                break;

            default:
                break;
        }
    }

    private void SetToGolem()
    {
        //specify starting golem stats here
        fireAttack = 1;
        waterAttack = 2;
        windAttack = 3;
        earthAttack = 4;
    }

    private void SetToBoss()
    {
        //specify starting boss stats here
        fireAttack = 1;
        waterAttack = 2;
        windAttack = 3;
        earthAttack = 4;
    }

    private void SetToDragon()
    {
        //specify starting dragon stats here
        fireAttack = 1;
        waterAttack = 2;
        windAttack = 3;
        earthAttack = 4;
    }

    private void SetToEnemy()
    {
        //specify starting enemy stats here
        fireAttack = 1;
        waterAttack = 2;
        windAttack = 3;
        earthAttack = 4;
    }

    //called from the attacked element's script to deal damage
    public bool TakeDamage(float damageAmount)
    {
        if (hp == 0) { return true; }

        hp = Mathf.Max(hp - damageAmount, 0);

        if (hp != 0) { return false; }

        //invoke some game over event maybe?
        return true;
    }   

    public void DealDamage(float damageAmount)
    {
        //if()
    }

    //returns the amount of damage the element could inflict at any given time
    //may be used together with the DealDamage method
    public float DamageAmount()
    {
        //fix this
        /*
        float damageAmount = (weakness == WeaknessType.FIRE ? weaknessFactor : 1f) * fireAttack
                           + (weakness == WeaknessType.WATER ? weaknessFactor : 1f) * waterAttack + windAttack + earthAttack;
        */
        return damageAmount;
    }
}
