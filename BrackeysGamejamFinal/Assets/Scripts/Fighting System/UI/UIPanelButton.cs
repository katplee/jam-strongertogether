using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPanelButton : UIObject, IPointerDownHandler
{
    private Button button;
    private Image dragonImage;

    private DragonType type;

    private const byte activeOpacity = 255;

    public override string Label
    {
        get { return name; }
    }

    private void Start()
    {
        button = GetComponent<Button>();
        dragonImage = GetComponent<Image>();

        //set the type of dragon in the panel tile
        SetDragonType();

        //set the interactability of the button based on the inventory
        InventorySave inventorySave = InventorySave.Instance.LoadInventoryData();
        InventoryData inventory = inventorySave.inventory;

        if(inventory.CountTamedDragons(type) > 0)
        {
            SetInteractability(true);
        }

    }

    public void SetInteractability(bool value)
    {
        //this means the dragon has been unlocked/tamed
        
        //set the interactability of the button
        button.interactable = value;

        //change the opacity of the image if interactable
        if (!value) { return; }

        Color32 color = dragonImage.color;
        color.a = activeOpacity;
        dragonImage.color = color;
    }

    private void SetDragonType()
    {
        int found = tag.IndexOf("Dragon");
        string element = tag.Substring(0, found).ToUpper();
        type = (DragonType)Enum.Parse(typeof(DragonType), element);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActionManager.Instance.SendButtonResponse(Label);
    }
}
