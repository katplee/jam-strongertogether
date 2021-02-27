using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Things to decide on:
 * *what will the weakness factor be equal to? (a variable in the TakeDamage method)
*/

public abstract class Element : MonoBehaviour
{
    public enum ElementType
    {
        GOLEM, BOSS, DRAGON, ENEMY, PLAYER
    }

    public abstract ElementType Type { get; }

    public WeaknessType Weakness { get; protected set; }

    protected int specialtyAttack = 15;
    protected int weaknessFactor = 2;

    public enum WeaknessType
    {
        FIRE, WATER, WIND, EARTH, NOTCONSIDERED
    }

    private Dragon.DragonType dType;
    public virtual Dragon.DragonType DType
    {
        get => dType;
        protected set => dType = value;
    }

    protected float hp;

    private float armor; //dragons do not have armor because they become the armor
    public virtual float Armor
    {
        get => armor;
        protected set => armor = value;
    }

    protected float remainder; //used in the computation of armor/hp to subtract
    //protected float damageAmount; //for testing only, because this will be computed

    //pertains to the amount of damage an element could cause
    //for example, if the fire attack is 3, the element could inflict 3 more damage points
    protected int fireAttack;
    protected int waterAttack;
    protected int windAttack;
    protected int earthAttack;

    protected float maxHP = 0;
    protected int hpMargin = 3;
    protected float hpLevelFactor = 10;

    protected float maxArmor = 0;


    protected virtual void Start()
    {
        InitializeAttributes();
        InitializeAttacks();
    }

    protected virtual void InitializeAttributes()
    {
        //set DType
        dType = Dragon.DragonType.NOTDRAGON;

        //each element's max HP is dependent on the level (temporary fix)
        float elementMaxHP = (GameManager.currLvl * hpLevelFactor) +
            Random.Range(-hpMargin, hpMargin);
        SetMaximumStat(ref maxHP, elementMaxHP);
        hp = maxHP;

        //each element's max HP is dependent on the level (temporary fix)
        maxArmor = 0;
        Armor = maxArmor;

        //determine weakness
        //in the meantime, weakness is randomly generated
        //  but for the final version, we might want to establish a correlation
        //  between the element and its weakness
        //weakness will be set at the moment of instantiation
        int weaknessInd = Random.Range(0, 4);
        Weakness = (WeaknessType)weaknessInd;
    }

    protected abstract void InitializeAttacks();

    public void SetMaximumStat(ref float maxStat, float newMaxStat)
    {
        maxStat = Mathf.Max(maxStat, newMaxStat);
    }

    //if the enemy is not a dragon
    //called to take damage by the enemy
    public virtual bool TakeDamage(float damageAmount, WeaknessType enemyWeakness = WeaknessType.NOTCONSIDERED)
    {
        //the armor gets damaged first!
        if (armor != 0)
        {
            //subtract the damage from the armor
            remainder = armor - damageAmount;
            armor = Mathf.Max(armor - damageAmount, 0);

            //if there is positive remainder (meaning there is still armor), return the element is not dead
            if (remainder > 0) { return false; }

            //if there is negative remainder
            else if (remainder < 0)
            {
                //subtract the remainder from the health
                hp = Mathf.Max((hp - Mathf.Abs(remainder)), 0);
            }
        }
        //if there is no more armor
        else
        {
            //if the hp is already zero, return the element is dead
            if (hp == 0) { return true; }

            //if not, then subtract the damage from the hp
            hp = Mathf.Max(hp - damageAmount, 0);
        }

        //if there is 0 remainder from the armor, check if hp is not depleted
        //if the hp was subtracted damage from
        //if the hp is not zero at the time of attack

        //if the hp is not depleted after subtracting, return the element is not dead
        if (hp != 0) { return false; }

        //else return the element is dead
        return true;
    }

    //if the enemy is a dragon (the boss)
    //called to take damage by the enemy
    public bool TakeDamage(float damageAmount, Dragon.DragonType dragonType)
    {
        /*OPTIONAL CODE:
         * I added this because maybe we would want to increase the effect of the damage
         * if the weakness of the enemy is the strength/kind of the dragon that attacks.
         * For example:
         *      ENEMY'S WEAKNESS: FIRE
         *      DRAGON TYPE: FIRE
         *      When the dragon attacks, the effect will be multiplied by 2 or something.
         *      And when the enemy attacks, since it's its weakness and the dragon's strength, the effect is diminished to half.
        */

        if (dragonType.ToString() == Weakness.ToString())
        {
            return TakeDamage(damageAmount * weaknessFactor);
        }
        else
        {
            return TakeDamage(damageAmount);
        }
    }

    //returns the amount of damage the element could inflict at any given time
    //may be used together with the TakeDamage method
    public float DamageAmount()
    {
        float damageAmount = fireAttack + waterAttack + windAttack + earthAttack;

        return damageAmount;
    }
}
