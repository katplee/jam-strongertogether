using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tameMenu : MonoBehaviour
{
    public Player player;
    public GameObject menu;
    private bool isChoosing = false;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isChoosing)
            {
                menu.SetActive(false);
                isChoosing = false;
                player.isChoosingTame = false;
            }
            else
            {
                menu.SetActive(true);
                isChoosing = true;
                player.isChoosingTame = true;
            }            
        }
    }
}
