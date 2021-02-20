using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// kat, 2/17/2021:
/// include all methods and parameters specific to any random enemy here
/// </summary>

public class Enemy : Element
{
    public override ElementType Type
    {
        get { return ElementType.ENEMY; }
    }

    //the base.Start() needs to be called at Awake
    //or else it would be too late for the updating of the stats
    private void Awake()
    {
        base.Start();
    }    
}
