using System.Collections;
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
                case Dragon.DragonType.BASE:
                    if (Inventory.instance.baseStones >= pStones)
                    {
                        Inventory.instance.baseStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case Dragon.DragonType.FIRE:
                    if (Inventory.instance.fireStones >= pStones)
                    {
                        Inventory.instance.fireStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case Dragon.DragonType.WATER:
                    if (Inventory.instance.waterStones >= pStones)
                    {
                        Inventory.instance.waterStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case Dragon.DragonType.WIND:
                    if (Inventory.instance.airStones >= pStones)
                    {
                        Inventory.instance.airStones -= pStones; //kat added this!
                        OnDragonCapture();
                        return;
                    }
                    break;

                case Dragon.DragonType.EARTH:
                    if (Inventory.instance.earthStones >= pStones)
                    {
                        Inventory.instance.earthStones -= pStones;
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
        Inventory.instance.AddDragon(dragon);

        //increase the dragon count
        PlayerData.Instance.dragonCount++;

        //save the dragon's script in the dragonsdata script
        DragonsData.Instance.SaveDragon(dragon);

        Destroy(gameObject);
    }
}
