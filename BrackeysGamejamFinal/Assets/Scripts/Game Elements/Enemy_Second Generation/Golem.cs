using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// kat, 2/17/2021:
/// include all methods and parameters specific to a golem here
/// </summary>

public class Golem : Enemy
{
    public override ElementType Type
    {
        get { return ElementType.GOLEM; }
    }

    private void Awake()
    {
        base.Start();
    }
}
