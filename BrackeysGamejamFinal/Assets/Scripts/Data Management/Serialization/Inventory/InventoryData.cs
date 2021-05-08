using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    //DATA OF STONES 
    public List<StoneData> baseStones = new List<StoneData>();
    public List<StoneData> fireStones = new List<StoneData>();
    public List<StoneData> waterStones = new List<StoneData>();
    public List<StoneData> earthStones = new List<StoneData>();
    public List<StoneData> airStones = new List<StoneData>();

    //DRAGONS
    public int dragons;

    //QUANTITY PER TYPE
    public INVDragon baseDragon;
    public INVDragon fireDragon;
    public INVDragon waterDragon;
    public INVDragon earthDragon;
    public INVDragon airDragon;

    public void AddDragon(DragonType type)
    {
        switch (type)
        {
            case DragonType.FIRE:
                fireDragon.tamed = true;
                fireDragon.quantity++;
                break;

            case DragonType.WATER:
                waterDragon.tamed = true;
                waterDragon.quantity++;
                break;

            case DragonType.AIR:
                airDragon.tamed = true;
                airDragon.quantity++;
                break;

            case DragonType.EARTH:
                earthDragon.tamed = true;
                earthDragon.quantity++;
                break;

            case DragonType.BASE:
                baseDragon.tamed = true;
                baseDragon.quantity++;
                break;

            default:
                break;
        }
    }

    public List<StoneData> ChooseStoneList(StoneType type)
    {
        switch (type)
        {
            case StoneType.FIRE:
                return fireStones;

            case StoneType.WATER:
                return waterStones;

            case StoneType.AIR:
                return airStones;

            case StoneType.EARTH:
                return earthStones;

            case StoneType.BASE:
                return baseStones;
        }

        return baseStones;
    }

    public int CountCollectedStones(StoneType type)
    {
        List<StoneData> list = ChooseStoneList(type);
        int count = 0;

        foreach (StoneData stone in list)
        {
            if(stone.collected == true) { count++; }
        }

        return count;
    }

    public void PopulateStoneList(StoneType type, StoneData stone)
    {
        List<StoneData> list = ChooseStoneList(type);
        list.Add(stone);
    }

    public void UseStone(StoneType type, int quantity)
    {
        
    }
}

[System.Serializable]
public class INVDragon
{
    public bool tamed;
    public int quantity;
}

[System.Serializable]
public class StoneData
{
    public StoneType type;

    public string name;

    public bool collected;
}
