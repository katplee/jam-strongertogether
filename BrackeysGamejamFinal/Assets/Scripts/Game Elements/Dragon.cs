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
        FIRE, WATER, WIND, EARTH
    }

    public DragonType DType { get; set; }

    private void InitializeAttributes()
    {
        //set dragon type (randomize?)
        //could be set depending on the level, etc.
        //for example 3 for an earth dragon
        int type = 3;
        DType = (DragonType)type;
    }

    private void Awake()
    {
        base.Start();
        InitializeAttributes();
    }
}

