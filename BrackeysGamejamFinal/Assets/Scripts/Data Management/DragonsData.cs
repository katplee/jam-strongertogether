using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class DragonsData : MonoBehaviour
{
    public static event Action NewDragonSaved;

    public static DragonsData Instance;

    public static List<List<Object>> dragonsStats = new List<List<Object>>();

    private Scene currentScene;
    private string attackScene = "AttackScene";

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
        code,
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
        { Stat.code, 0},
        { Stat.dragon_type, 1 },
        { Stat.hp, 2 },
        { Stat.armor, 3 },
        { Stat.damage_amount, 4 },
        { Stat.weakness, 5 },
        { Stat.weakness_factor, 6 },
        { Stat.fire_attack, 7 },
        { Stat.water_attack, 8 },
        { Stat.wind_attack, 9 },
        { Stat.earth_attack, 10 },
    };

    private void Start()
    {
        SceneTransition.JustAfterSceneTransition += LogDragonStats;
        NewDragonSaved += LogDragonStats;

        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LogDragonStats()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == attackScene) { return; }

        List<Object> lastLoggedDragon = dragonsStats[dragonsStats.Count - 1];

        Debug.Log($"{(Stat)0} : {lastLoggedDragon[0]}");
        Debug.Log($"{(Stat)1} : {lastLoggedDragon[1]}");
        Debug.Log($"{(Stat)2} : {lastLoggedDragon[2]}");
        Debug.Log($"{(Stat)3} : {lastLoggedDragon[3]}");
        Debug.Log($"{(Stat)4} : {lastLoggedDragon[4]}");
        Debug.Log($"{(Stat)5} : {lastLoggedDragon[5]}");
        Debug.Log($"{(Stat)6} : {lastLoggedDragon[6]}");
        Debug.Log($"{(Stat)7} : {lastLoggedDragon[7]}");
        Debug.Log($"{(Stat)8} : {lastLoggedDragon[8]}");
        Debug.Log($"{(Stat)9} : {lastLoggedDragon[9]}");
    }

    public void SaveDragon(Dragon dragon)
    {
        //create a new temporary list to save each dragon's stats
        List<Object> dragonStats = new List<Object>()
        {
            dragon,
            dragon.DType,
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
        NewDragonSaved?.Invoke();
    }
}
