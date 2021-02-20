using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// kat, 2/17/2021:
/// include all methods and parameters specific to a boss here
/// </summary>

public class Boss : Enemy
{
    public override ElementType Type
    {
        get { return ElementType.BOSS; }
    }

    private void Awake()
    {
        base.Start();
    }
}
