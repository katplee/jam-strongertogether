using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class DragonsData : MonoBehaviour
{
    public static DragonsData Instance;

    public static List<List<Object>> dragonsStats = new List<List<Object>>();

    ///format of List<float>
    ///List<float> dragonStats = new List<float>()
    ///     {
    ///         dragon type,
    ///         hp,
    ///         armor,
    ///         damage amount,
    ///         weakness,
    ///         weakness factor,
    ///         fire attack,
    ///         water attack,
    ///         wind attack,
    ///         earth attack
    ///     }

    private enum Stat
    {
        dragon_type,
        hp,
        armor,
        damage_amount,
        weakness,
        weakness_factor,
        fire_attack,
        water_attack,
        wind_attack,
        earth_attack            
    }

    private Dictionary<Stat, int> stat = new Dictionary<Stat, int>()
    {
        { Stat.dragon_type, 0 },
        { Stat.hp, 1 },
        { Stat.armor, 2 },
        { Stat.damage_amount, 3 },
        { Stat.weakness, 4 },
        { Stat.weakness_factor, 5 },
        { Stat.fire_attack, 6 },
        { Stat.water_attack, 7 },
        { Stat.wind_attack, 8 },
        { Stat.earth_attack, 9 },
    };

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SaveDragons();
        //Debug.Log(dragonsStats.Count);
        //Debug.Log(dragonsStats[0][stat[Stat.dragon_type]]);
        //Debug.Log(dragonsStats[1][stat[Stat.dragon_type]]);
    }

    private void SaveDragons()
    {
        Dragon[] dragons = FindObjectsOfType<Dragon>();

        foreach (Dragon dragon in dragons)
        {
            //create a new temporary list to save each dragon's stats
            List<Object> dragonStats = new List<Object>()
            {
                //dragon.DType,
                dragon.hp,
                dragon.armor,
                dragon.DamageAmount(),
                dragon.weakness,
                dragon.weaknessFactor,
                dragon.fireAttack,
                dragon.waterAttack,
                dragon.windAttack,
                dragon.earthAttack
            };

            dragonsStats.Add(dragonStats);
        }
    }
}
