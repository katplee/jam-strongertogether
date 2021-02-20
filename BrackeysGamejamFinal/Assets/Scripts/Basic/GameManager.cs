using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("General")]
    private bool onMission;
    public GameObject winGameUI;
    public GameObject winLvlUI;
    public GameObject gameOverUI;

    [Header("Level Control")]
    public int currLvl = 0;

    private void Start()
    {
        if(winGameUI!=null)
            winGameUI.SetActive(false);
        if(gameOverUI!=null)
            gameOverUI.SetActive(false);
        if (winLvlUI!= null)
            winLvlUI.SetActive(false);
    }

    //will act as mission manager alsoi

    public void EndMission()
    {
        winGameUI.SetActive(true);
    }

    public void winCurrentLevel()
    {
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