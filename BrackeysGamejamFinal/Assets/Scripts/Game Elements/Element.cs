using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <notes>
/// kat, 2/17/2021:
/// *confirm whether attacks could be increased
/// </notes>

public class Element : MonoBehaviour
{
    public enum ElementType
    {
        GOLUM, BOSS, DRAGON, ENEMY
    }

    public enum WeaknessType
    {
        FIRE, WATER, WIND, EARTH
    }

    public ElementType Type { get; set; }

    public int HP { get; set; }
    public int Armor { get; set; }

    //pertains to the amount of damage an element could cause
    //for example, if the fire attack is 3, the element could inflict 3 more damage points
    public int FireAttack { get; set; }
    public int WaterAttack { get; set; }
    public int WindAttack { get; set; }
    public int EarthAttack { get; set; }

    public WeaknessType Weakness { get; set; }

    protected int maxHP = 100;

    public Element(ElementType type)
    {
        Type = type;
        InitializeCommonAttributes();
        InitializeUniqueAttributes(type);
    }

    private void InitializeCommonAttributes()
    {
        //set starting hp to full (assumed full scale is 100)
        HP = maxHP;

        //set starting armor to full (assumed full scale is 100)
        Armor = 100;

        //determine weakness
        //in the meantime, weakness is randomly generated
        //  but for the final version, we might want to establish a correlation
        //  between the element and its weakness
        int weakness = 2;
        Weakness = (WeaknessType)weakness;
    }

    private void InitializeUniqueAttributes(ElementType type)
    {
        switch (type)
        {
            case ElementType.GOLUM:
                SetToGolum();
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

    private void SetToGolum()
    {
        //specify starting golum stats here
        FireAttack = 1;
        WaterAttack = 2;
        WindAttack = 3;
        EarthAttack = 4;
    }

    private void SetToBoss()
    {
        //specify starting boss stats here
        FireAttack = 1;
        WaterAttack = 2;
        WindAttack = 3;
        EarthAttack = 4;
    }

    private void SetToDragon()
    {
        //specify starting dragon stats here
        FireAttack = 1;
        WaterAttack = 2;
        WindAttack = 3;
        EarthAttack = 4;
    }

    private void SetToEnemy()
    {
        //specify starting enemy stats here
        FireAttack = 1;
        WaterAttack = 2;
        WindAttack = 3;
        EarthAttack = 4;
    }

    //called from the attacked element's script to deal damage
    public void DealDamage(int damageAmount)
    {
        if (HP == 0) { return; }

        HP = Mathf.Max(HP - damageAmount, 0);

        if (HP != 0) { return; }

        //invoke some game over event maybe?
    }

    //returns the amount of damage the element could inflict at any given time
    //may be used together with the DealDamage method
    public int DamageAmount()
    {
        int damageAmount = FireAttack + WaterAttack + WindAttack + EarthAttack;

        return damageAmount;
    }
}
