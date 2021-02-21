using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// kat, 2/17/2021:
/// include all methods and parameters specific to a dragon here
/// </summary>

public class Dragon : Element
{
    public enum DragonType
    {
        FIRE, WATER, WIND, EARTH, BASE, NOTDRAGON
    }

    #region Kat's region
    public override DragonType DType
    {
        get => _dType;
        set => _dType = value;
    }
    #endregion

    public override ElementType Type
    {
        get { return ElementType.DRAGON; }
    }

    public float dragonImmunity = 0.5f; //kat added this!

    //LEVELING SYSTEM
    private int Lvl = 1;
    private float XP = 0;

    [Header("Leveling System")]
    public float xpPerMinute = 10f;
    public float xpPerWonFight = 120f;
    public float xpPerLvl = 100f;
    public int maxLvl = 5;

    public TamingReqs tamingReqs;

    private void Awake()
    {
        InitializeDragonAttributes();
        InitializeUniqueAttributes(Type);        
    }

    protected override void Start()
    {
        //DO NOT ERASE THIS METHOD! This prevents the base.Start() from being called!
        //Do not load the base.Start() method so the weakness will not be set.
    }

    private void InitializeDragonAttributes()
    {
        switch (DType)
        {
            case DragonType.FIRE:
                weakness = WeaknessType.WATER;
                break;

            case DragonType.WATER:
                weakness = WeaknessType.FIRE;
                break;

            case DragonType.WIND:
                weakness = WeaknessType.EARTH;
                break;

            case DragonType.EARTH:
                weakness = WeaknessType.WIND;
                break;

            case DragonType.BASE:
                weakness = WeaknessType.NOTCONSIDERED;
                break;
        }        
    }

    public void Tame()
    {

    }

    /*
    #region OPTIONAL CODE
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
    #endregion
    */
}

