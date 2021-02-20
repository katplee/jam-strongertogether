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

    private Dragon waterDragon = null;
    public GameObject wDragoPrefab;

    private Dragon earthDragon = null;
    public GameObject eDragoPrefab;

    private Dragon airDragon = null;
    public GameObject aDragoPrefab;

    private Dragon baseDragon = null;
    public GameObject bDragoPrefab;

    public Dragon _dragon;
    

    void Awake() //Singleton pattern!
    {
        DontDestroyOnLoad(this);
        if (instance != null)
        {
            Debug.LogError("More than one Inventory in the scene");
            return;
        }

        instance = this;
    }

    public void AddDragon(Dragon d)
    {
        //Dragon _dragon;

        switch (d.DType)
        {
            case Dragon.DragonType.FIRE:
                _dragon = fDragoPrefab.GetComponent<Dragon>();
                break;
            case Dragon.DragonType.WATER:
                _dragon = wDragoPrefab.GetComponent<Dragon>();
                break;
            case Dragon.DragonType.EARTH:
                _dragon = eDragoPrefab.GetComponent<Dragon>();
                break;
            case Dragon.DragonType.WIND:
                _dragon = aDragoPrefab.GetComponent<Dragon>();
                break;
            case Dragon.DragonType.BASE:
                _dragon = bDragoPrefab.GetComponent<Dragon>();
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
