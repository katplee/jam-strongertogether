using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Things to decide on:
 * *to make the fight scene pace faster, should the hp of the enemy be dependent on the level?
 *      check temporary fix to this.
 */

public class Enemy : Element
{
    public override ElementType Type
    {
        get { return ElementType.ENEMY; }
    }

    protected override void InitializeAttributes()
    {
        //set starting hp to full (assumed full scale is 100)
        hp = GameManager.currLvl;
        maxHP = hp;
        
        //set starting armor to full (assumed full scale is 100)
        //the starting armor of elements will be dependent on the level
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

    protected override void InitializeAttacks()
    {

    }

    //the base.Start() needs to be called at Awake
    //or else it would be too late for the updating of the stats
    private void Awake()
    {
        base.Start();
    }    
}
