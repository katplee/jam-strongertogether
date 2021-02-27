using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnLevelChange;

    public static GameManager Instance { get; private set; }

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

        OnLevelChange += WinCurrentLevel;

        ConvertToPersistentData();
    }

    private void OnDestroy()
    {
        OnLevelChange -= WinCurrentLevel;
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

    private void Update()
    {
        
    }


    //will act as mission manager alsoi

    public void EndMission()
    {
        winGameUI.SetActive(true);
    }

    public void WinCurrentLevel()
    {
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
}