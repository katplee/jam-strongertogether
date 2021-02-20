using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    //stones
    //[HideInInspector]
    public int fireStones = 0;
    //[HideInInspector]
    public int earthStones = 0;
    //[HideInInspector]
    public int waterStones = 0;
    //[HideInInspector]
    public int airStones = 0;
    //[HideInInspector]
    public  int baseStones = 0;

    [Header("Dragons")]
    private Dragon fireDragon = null;
    public GameObject fDragoPrefab;
    public bool hasFireD = false;

    private Dragon waterDragon = null;
    public GameObject wDragoPrefab;
    public bool hasWaterD = false;

    private Dragon earthDragon = null;
    public GameObject eDragoPrefab;
    public bool hasEarthD = false;

    private Dragon airDragon = null;
    public GameObject aDragoPrefab;
    public bool hasAirD = false;

    private Dragon baseDragon = null;
    public GameObject bDragoPrefab;
    public bool hasBaseD = false;

    public Dragon _dragon;   


    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDragon(Dragon d)
    {
        //Adding dragon!!       

        switch (d.DType)
        {
            case Dragon.DragonType.FIRE:
                _dragon = fDragoPrefab.GetComponent<Dragon>();
                hasFireD = true;
                break;
            case Dragon.DragonType.WATER:
                _dragon = wDragoPrefab.GetComponent<Dragon>();
                hasWaterD = true;
                break;
            case Dragon.DragonType.EARTH:
                _dragon = eDragoPrefab.GetComponent<Dragon>();
                hasEarthD = true;
                break;
            case Dragon.DragonType.WIND:
                _dragon = aDragoPrefab.GetComponent<Dragon>();
                hasAirD = true;
                break;
            case Dragon.DragonType.BASE:
                _dragon = bDragoPrefab.GetComponent<Dragon>();
                hasBaseD = true;
                break;
        }

        //_dragon = fDragoPrefab.GetComponent<Dragon>(); kat: should we erase this? it's messing up the _dragon variable.
        _dragon.DType = d.DType;
        _dragon.xpPerMinute = d.xpPerMinute;
        _dragon.xpPerWonFight = d.xpPerWonFight;
        _dragon.xpPerLvl = d.xpPerLvl;
        _dragon.maxLvl = d.maxLvl;
        _dragon.tamingReqs.pStones = d.tamingReqs.pStones;
        _dragon.tamingReqs.pStonesReq = null;
                
        switch (d.DType)
        {
            case Dragon.DragonType.FIRE:
                fireDragon = _dragon;
                break;
            case Dragon.DragonType.WATER:
                waterDragon = _dragon;
                break;
            case Dragon.DragonType.EARTH:
                earthDragon = _dragon;
                break;
            case Dragon.DragonType.WIND:
                airDragon = _dragon;
                break;
            case Dragon.DragonType.BASE:
                baseDragon = _dragon;
                break;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<tameMenu>().UpdateMenu();
    }

    public void AddStone(PStone stone)
    {
        switch (stone.type)
        {
            case PStone.StoneType.BASE:
                baseStones++;
                break;

            case PStone.StoneType.FIRE:
                fireStones++;
                break;

            case PStone.StoneType.WATER:
                waterStones++;
                break;

            case PStone.StoneType.WIND:
                airStones++;
                break;

            case PStone.StoneType.EARTH:
                earthStones++;
                break;
        }
    }

    public void UseStone(PStone.StoneType type, int amt)
    {
        switch (type)
        {
            case PStone.StoneType.BASE:
                baseStones++;
                break;
            case PStone.StoneType.FIRE:
                fireStones++;
                break;
            case PStone.StoneType.WATER:
                waterStones++;
                break;
            case PStone.StoneType.WIND:
                airStones++;
                break;
            case PStone.StoneType.EARTH:
                earthStones++;
                break;
        }
    }
}
