using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// kat, 2/17/2021:
/// include all methods and parameters specific to a dragon here
/// </summary>

public class Dragon : Element
{
    public override ElementType Type
    {
        get { return ElementType.DRAGON; }
    }

    public enum DragonType
    {
        FIRE, WATER, WIND, EARTH, NOTDRAGON
    }
        
    protected override DragonType DType
    {
        get => _dType;
        set => _dType = value;
    }
    public int dType;

    private float dragonImmunity = 0.5f;

    private void Awake()
    {
        base.Start();        
    }

    protected new void Start()
    {
        InitializeAttributes();
    }

    private void InitializeAttributes()
    {
        //set dragon type (randomize?)
        //could be set depending on the level, etc.
        //for example 3 for an earth dragon
        dType = Random.Range(0, 4);
        Debug.Log(dType);
        DType = (DragonType)dType;
        Debug.Log(DType);
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
}

