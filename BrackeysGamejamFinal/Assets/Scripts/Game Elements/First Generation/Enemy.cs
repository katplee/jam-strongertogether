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

    protected override void InitializeAttacks()
    {
        int currentLvl = GameManager.currLvl;
        int attack = Random.Range(1, GameManager.currLvl * attackMargin + 1);

        baseAttack = ((currentLvl == GameManager.baseLevel) ? specialtyAttackMultiplier : 0) * attack;
        fireAttack = ((currentLvl == GameManager.fireLevel) ? specialtyAttackMultiplier : 0) * attack;
        waterAttack = ((currentLvl == GameManager.waterLevel) ? specialtyAttackMultiplier : 0) * attack;
        windAttack = ((currentLvl == GameManager.windLevel) ? specialtyAttackMultiplier : 0) * attack;
        earthAttack = ((currentLvl == GameManager.earthLevel) ? specialtyAttackMultiplier : 0) * attack;
    }        
}
