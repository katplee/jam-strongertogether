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

public abstract class Element : MonoBehaviour
{
    public enum ElementType
    {
        GOLEM, BOSS, DRAGON, ENEMY, PLAYER
    }

    public enum WeaknessType
    {
        FIRE, WATER, WIND, EARTH, NOTCONSIDERED
    }

    public abstract ElementType Type { get; }

    protected Dragon.DragonType _dType;
    protected virtual Dragon.DragonType DType 
    {
        get => Dragon.DragonType.NOTDRAGON;
        set => _dType = value;
    }

    public float hp;
    public float armor;

    public float damageAmount; //for testing only, because this will be computed
    public WeaknessType weakness;
    public float weaknessFactor = 2.0f;

    //pertains to the amount of damage an element could cause
    //for example, if the fire attack is 3, the element could inflict 3 more damage points
    public int fireAttack;

    public int waterAttack;

    public int windAttack;

    public int earthAttack;

    protected float maxHP = 100;

    protected void Start()
    {
        InitializeCommonAttributes();
        InitializeUniqueAttributes(Type);
    }

    protected void InitializeCommonAttributes()
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
        int weaknessInd = Random.Range(0, 4);
        weakness = (WeaknessType)weaknessInd;
    }

    protected void InitializeUniqueAttributes(ElementType type)
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

            case ElementType.PLAYER:
                SetToPlayer();
                break;

            default:
                break;
        }
    }

    private void SetToGolem()
    {
        //specify starting golem stats here
        fireAttack = 8;
        waterAttack = 8;
        windAttack = 8;
        earthAttack = 8;
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

    private void SetToPlayer()
    {
        //specify starting enemy stats here
        fireAttack = 1;
        waterAttack = 2;
        windAttack = 3;
        earthAttack = 4;
    }

    //if the enemy is not a dragon
    //called to take damage by the enemy
    public virtual bool TakeDamage(float damageAmount, WeaknessType enemyWeakness = WeaknessType.NOTCONSIDERED)
    {
        if (hp == 0) { return true; }

        hp = Mathf.Max(hp - damageAmount, 0);

        if (hp != 0) { return false; }

        //invoke some game over event maybe?
        return true;
    }

    //if the enemy is a dragon
    //called to take damage by the enemy
    public bool TakeDamage(Dragon.DragonType dragonType, float damageAmount)
    {
        /*OPTIONAL CODE:
         * I added this because maybe we would want to increase the effect of the damage
         * if the weakness of the enemy is the strength/kind of the dragon that attacks.
         * For example:
         *      ENEMY'S WEAKNESS: FIRE
         *      DRAGON TYPE: FIRE
         *      When the dragon attacks, the effect will be multiplied by 2 or something.
         *      And when the enemy attacks, since it is it's weakness and the dragon's strength, the effect is diminished to half.
        */

        #region OPTIONAL CODE

        if (dragonType.ToString() == weakness.ToString())
        {
            return TakeDamage(damageAmount * weaknessFactor);
        }
        else
        {
            return TakeDamage(damageAmount);
        }

        #endregion
    }

    //returns the amount of damage the element could inflict at any given time
    //may be used together with the TakeDamage method
    public float DamageAmount()
    {
        //fix this
        /*
        float damageAmount = 
            (weakness == WeaknessType.FIRE ? weaknessFactor : 1f) * fireAttack +
            (weakness == WeaknessType.WATER ? weaknessFactor : 1f) * waterAttack +
            (weakness == WeaknessType.FIRE ? weaknessFactor : 1f) * windAttack +
            (weakness == WeaknessType.FIRE ? weaknessFactor : 1f) * earthAttack;
        */
        return damageAmount;
    }
}
