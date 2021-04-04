﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TamingReqs : MonoBehaviour
{
    [Header("Taming System")]
    public int pStones = 2;
    
    ///refers to the panel containing the reqts and all
    ///this is causing all the errors.
    ///it suddenly disappears and reappears on the prefab
    ///should we make this private?
    /// vvvvvvvvvvvvvvvvvvvvvvvvv
    public GameObject pStonesReq;
    
    public Dragon dragon; //refers to the game object's Dragon.cs script

    [Header("UI")]    
    public Text reqText;
    public Sprite reqStone;
    public Image imgStone;

    void Start()
    {
        //hides the reqts
        if (pStonesReq != null)
        {
            pStonesReq.SetActive(false);
            reqText.text = pStones.ToString();
            imgStone.sprite = reqStone;
        }               
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (dragon.DType)
            {
                case DragonType.BASE:
                    if (Inventory.Instance.baseStones >= pStones)
                    {
                        Inventory.Instance.baseStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case DragonType.FIRE:
                    if (Inventory.Instance.fireStones >= pStones)
                    {
                        Inventory.Instance.fireStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case DragonType.WATER:
                    if (Inventory.Instance.waterStones >= pStones)
                    {
                        Inventory.Instance.waterStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case DragonType.WIND:
                    if (Inventory.Instance.airStones >= pStones)
                    {
                        Inventory.Instance.airStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case DragonType.EARTH:
                    if (Inventory.Instance.earthStones >= pStones)
                    {
                        Inventory.Instance.earthStones -= pStones;
                        OnDragonCapture();
                        return;
                    }
                    break;
            }

            //if the stones are not enough, just display the reqts panel
            pStonesReq.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pStonesReq.SetActive(false);
        }
    }

    private void OnDragonCapture()
    {
        //add the dragon to the inventoru
        Inventory.Instance.AddDragon(dragon);

        //destroy the dragon in the scene
        Destroy(gameObject);
    }
}