using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonInstance : MonoBehaviour
{
    public Dragon dragon;

    //attributes to that will be set per character
    [SerializeField] private Element.WeaknessType weakness;

    private void Start()
    {
    }

}
