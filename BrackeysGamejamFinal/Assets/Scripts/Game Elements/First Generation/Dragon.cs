using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Things to decide on:
 * *will the player increase in xp as well? Or is it just the dragon?
 * *how does the dragon's hp change with its xp?
 * *at what instances will the xpPerMinute timer count up?
 *      ie. will the timer continuously count up only when the dragon has been tamed?
 *          will the timer stop when the dragon is in a battle with its owner? etc.
 * 
 * Things to remember:
 * *maybe it would be nice to also include the weaknesses, etc. on the player HUD
 */

[System.Serializable]
public enum DragonType
{
    FIRE, WATER, WIND, EARTH, BASE, NOTDRAGON
}

public class Dragon : Element
{
    public TamingReqs tamingReqs;
    public override ElementType Type
    {
        get { return ElementType.DRAGON; }
    }    

    private const string baseDragonTag = "BaseDragon";
    private const string fireDragonTag = "FireDragon";
    private const string waterDragonTag = "WaterDragon";
    private const string windDragonTag = "WindDragon";
    private const string earthDragonTag = "EarthDragon";

    protected float dragonImmunity = 0.5f;

    [Header("Leveling System")]
    protected bool isTame = false; //boolean condition connected to leveling system
    protected float xpPerMinute = 10f;
    protected float xpPerWonFight = 120f;
    protected float xpPerLvl = 100f;
    protected int maxLvl = 5;
    protected float maxXP = 0;
    protected float xp = 0;

    public override float Armor
    {
        get => 0f;
        protected set => Armor = 0f;
    }

    protected override void Awake()
    {
        SubscribeEvents();
        base.Awake();
    }

    protected override void OnDestroy()
    {
        UnsubscribeEvents();
        base.OnDestroy();
    }

    protected override void InitializeAttributes()
    {
        SetDragonTypeWeakness();

        //each element's max HP is dependent on the level (temporary fix)
        float elementMaxHP = (GameManager.currLvl * hpLevelFactor) +
            Random.Range(-hpMargin, hpMargin);
        SetStatMaximum(ref maxHP, elementMaxHP);
        hp = maxHP;

        //dragon's maxXP is set to 0 at first
        maxXP = 0;
        xp = maxXP;
    }

    public override void InitialSerialization()
    {
        
    }

    protected override void InitializeSerialization()
    {
        
    }

    public override void InitializeDeserialization()
    {
        
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
        baseAttack = (DType == DragonType.BASE) ? specialtyAttack : 0;
    }

    /*
     * This method will be called when:
     * METHOD CALLER: not a dragon | ELEMENT WHOSE TAKEDAMAGE METHOD IS CALLED: dragon
     *      call format: TakeDamage(float damageAmount, WeaknessType callerWeakness, GameObject callerGO)
     * METHOD CALLER: dragon/player fused with a dragon | ELEMENT WHOSE TAKEDAMAGE METHOD IS CALLED: dragon
     *      call format: TakeDamage(float damageAmount, WeaknessType callerWeakness, GameObject callerGO)
     */
    public override bool TakeDamage(float damageAmount, WeaknessType enemyWeakness, GameObject enemyGO)
    {
        float effDamageAmount = (enemyWeakness.ToString() == DType.ToString()) ?
            damageAmount * dragonImmunity : damageAmount;

        //if caller is also a dragon (dragon vs dragon)
        if (enemyGO.TryGetComponent(out Dragon dragon)) { return TakeDamage(effDamageAmount, dragon.DType); }
        
        //if caller is not a dragon (non-dragon vs dragon)
        return base.TakeDamage(effDamageAmount);
    }

    public void Tame()
    {

    }

    private void OnLevelChange()
    {
        //isTame becomes true if dragon is player's pet
        if (!isTame) { return; }

        //set xp maximum to new value
        SetStatMaximum(ref maxXP, maxXP + xpPerLvl);

        //add xpPerLvl to current xp;
        xp += xpPerLvl;
    }

    private void OnFightEnd()
    {
        //isTame becomes true if dragon is player's pet
        if (!isTame) { return; }

        //set xp maximum to new value
        SetStatMaximum(ref maxXP, maxXP + xpPerWonFight);

        //add xpPerLvl to current xp;
        xp += xpPerWonFight;
    }

    private void SubscribeEvents()
    {
        GameManager.OnLevelWin += OnLevelChange;
        FightManager.OnFightEnd += OnFightEnd;
    }

    private void UnsubscribeEvents()
    {
        GameManager.OnLevelWin -= OnLevelChange;
        FightManager.OnFightEnd -= OnFightEnd;
    }
}

