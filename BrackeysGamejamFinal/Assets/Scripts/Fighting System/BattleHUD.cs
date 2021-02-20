using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text playerLevelText;
    public Image hpStatsBar;
    public Image armorStatsBar;

    public void UpdateHUD<T>(T element)
        where T : Element
    {
        playerNameText.text = element.Type.ToString();
        playerLevelText.text = "Lvl " + element.armor.ToString();
        Debug.Log(element.hp / 100);
        hpStatsBar.fillAmount = element.hp / 100;
        armorStatsBar.fillAmount = element.armor / 100;
    }

    public void UpdateHP<T>(float hp)
        where T : Element
    {
        hpStatsBar.fillAmount = hp / 100;
    }

}
