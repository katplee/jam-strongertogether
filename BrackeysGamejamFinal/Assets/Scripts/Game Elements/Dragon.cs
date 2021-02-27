using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Things to decide on:
 * *will the player increase in xp as well? Or is it just the dragon?
 * *how does the hp related with the xp?
 * 
 * Things to remember:
 * *maybe it would be nice to also include the weaknesses, etc. on the player HUD
 */

public class Dragon : Element
{
    public TamingReqs tamingReqs;
    public override ElementType Type
    {
        get { return ElementType.DRAGON; }
    }

    public enum DragonType
    {
        FIRE, WATER, WIND, EARTH, BASE, NOTDRAGON
    }

    private const string baseDragonTag = "BaseDragon";
    private const string fireDragonTag = "FireDragon";
    private const string waterDragonTag = "WaterDragon";
    private const string windDragonTag = "WindDragon";
    private const string earthDragonTag = "EarthDragon";

    public override DragonType DType
    {
        get => base.DType;
        protected set => base.DType = value;
    }

    protected float dragonImmunity = 0.5f;
    
    [Header("Leveling System")]
    public float xpPerMinute = 10f;
    public float xpPerWonFight = 120f;
    public float xpPerLvl = 100f;
    public int maxLvl = 5;

    private int lvl = 0; //set the level at start
    private float xp = 0;
    public override float Armor
    {
        get => base.Armor;
        protected set => base.Armor = 0;
    }

    protected override void InitializeAttributes()
    {
        //set starting hp to full (assumed full scale is 100)
        maxHP = 100;
        hp = maxHP;

        SetDragonTypeWeakness();
    }

    private void SetDragonTypeWeakness()
    {
        string tag = gameObject.tag;

        switch (tag)
        {
            case baseDragonTag:
                DType = DragonType.BASE;
                int weaknessInd = Random.Range(0, 4);
                Weakness = (WeaknessType)weaknessInd;
                break;

            case fireDragonTag:
                DType = DragonType.FIRE;
                Weakness = WeaknessType.WATER;
                break;

            case waterDragonTag:
                DType = DragonType.WATER;
                Weakness = WeaknessType.FIRE;
                break;

            case windDragonTag:
                DType = DragonType.WIND;
                Weakness = WeaknessType.EARTH;
                break;

            case earthDragonTag:
                DType = DragonType.EARTH;
                Weakness = WeaknessType.WIND;
                break;

            default:
                break;
        }
    }   

    protected override void InitializeAttacks()
    {
        fireAttack = (DType == DragonType.FIRE) ? specialtyAttack : 0;
        waterAttack = (DType == DragonType.WATER) ? specialtyAttack : 0;
        windAttack = (DType == DragonType.WIND) ? specialtyAttack : 0;
        earthAttack = (DType == DragonType.EARTH) ? specialtyAttack : 0;
    }

    public override bool TakeDamage(float damageAmount, WeaknessType enemyWeakness)
    {
        float effDamageAmount = (enemyWeakness.ToString() == DType.ToString()) ?
            damageAmount * dragonImmunity : damageAmount;

        if (hp == 0) { return true; }

        hp = Mathf.Max(hp - effDamageAmount, 0);

        if (hp != 0) { return false; }

        //invoke some game over event maybe?
        return true;
    }

    public void Tame()
    {

    }
}

