using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class DragonsData : MonoBehaviour
{
    public static event Action NewDragonSaved;

    public static DragonsData Instance;

    public static List<List<Object>> dragonsStats = new List<List<Object>>();
    public List<List<Object>> dragonsList = new List<List<Object>>();

    [TextArea(15, 15)]
    public List<Object> dragonList;

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

        if(dragonsStats.Count == 0) { return; }

        var sorted = dragonsStats.OrderBy(list => list[0]);

        foreach(var sort in sorted)
        {
            dragonList.Add(sort);
        }

        /*
        List<Object> lastLoggedDragon = dragonsStats[dragonsStats.Count - 1];

        string lastLoggedDragonText = 
            $"{(Stat)0} : {lastLoggedDragon[0]}\n" +
            $"{(Stat)1} : {lastLoggedDragon[1]}\n" +
            $"{(Stat)2} : {lastLoggedDragon[2]}\n" +
            $"{(Stat)3} : {lastLoggedDragon[3]}\n" +
            $"{(Stat)4} : {lastLoggedDragon[4]}\n" +
            $"{(Stat)5} : {lastLoggedDragon[5]}\n" +
            $"{(Stat)6} : {lastLoggedDragon[6]}\n" +
            $"{(Stat)7} : {lastLoggedDragon[7]}\n" +
            $"{(Stat)8} : {lastLoggedDragon[8]}\n" +
            $"{(Stat)9} : {lastLoggedDragon[9]}";

        dragonList.Add(lastLoggedDragonText);
        */
    }

    public void SaveDragon(Dragon dragon)
    {
        //create a new temporary list to save each dragon's stats
        List<Object> dragonStats = new List<Object>()
        {
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
