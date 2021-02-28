using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnLevelChange;

    public static GameManager Instance { get; private set; }

    public const int baseLevel = 1;
    public const int fireLevel = 2;
    public const int waterLevel = 3;
    public const int windLevel = 4;
    public const int earthLevel = 5;

    [Header("General")]
    private bool onMission;
    public GameObject winGameUI;
    public GameObject winLvlUI;
    public GameObject gameOverUI;

    [Header("Level Control")]
    public static int currLvl = 1;

    private void Start()
    {
        /*
            if(winGameUI!=null)
                winGameUI.SetActive(false);
            if(gameOverUI!=null)
                gameOverUI.SetActive(false);
            if (winLvlUI!= null)
                winLvlUI.SetActive(false);
        */

        SubscribeEvents();
        ConvertToPersistentData();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void ConvertToPersistentData()
    {
        DontDestroyOnLoad(this);

        //to avoid duplication of game objects when transitioning between scenes
        //this is not the code to make the class a singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
       
    //will act as mission manager also
    public void EndMission()
    {
        winGameUI.SetActive(true);
    }

    public void WinCurrentLevel()
    {
        //increase the level number
        currLvl++;

        winLvlUI.SetActive(true);
    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
    }

    public virtual void EndDialogue()
    {
        Debug.Log("Endof dialogue");
    }

    private void SubscribeEvents()
    {
        OnLevelChange += WinCurrentLevel;
    }

    private void UnsubscribeEvents()
    {
        OnLevelChange -= WinCurrentLevel;
    }
}