using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : UIObject
{
    private TMP_Text text;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void ChangeText(string level)
    {
        text.text = level;
    }

    private void SubscribeEvents()
    {
        
    }

}
