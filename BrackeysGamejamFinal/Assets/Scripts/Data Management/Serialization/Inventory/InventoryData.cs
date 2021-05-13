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
    public DragonData baseDragons = new DragonData();
    public DragonData fireDragons = new DragonData();
    public DragonData waterDragons = new DragonData();
    public DragonData earthDragons = new DragonData();
    public DragonData airDragons = new DragonData();

    #region Dragons

    public void AddDragon(Dragon dragon)
    {
        DragonData data = ChooseDragonType(dragon.DType);

        data.tamed = true;
        data.quantity++;
        data.list.Add(dragon);
    }

    public DragonData ChooseDragonType(DragonType type)
    {
        switch (type)
        {
            case DragonType.FIRE:
                return fireDragons;

            case DragonType.WATER:
                return waterDragons;

            case DragonType.AIR:
                return airDragons;

            case DragonType.EARTH:
                return earthDragons;

            case DragonType.BASE:
                return baseDragons;

            case DragonType.NOTDRAGON:
                return null;
        }

        return null;
    }

    #endregion

    #region Stones
    
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

        return null;
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
        List<StoneData> list = ChooseStoneList(type);
        int i = 0;
        int count = 0;

        while(count < quantity)
        {
            if (list[i].collected == true) 
            {
                list.Remove(list[i]);
                count++;
            }
            else { i++; }
        }
    }

    #endregion
}

[System.Serializable]
public class StoneData
{
    public StoneType type;

    public string name;

    public bool collected;
}
