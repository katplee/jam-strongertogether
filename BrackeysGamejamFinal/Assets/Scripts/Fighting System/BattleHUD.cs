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

    public void UpdateHUD(Element element)
    {
        playerNameText.text = element.Type.ToString();
        playerLevelText.text = "Lvl " + element.Armor.ToString();
        hpStatsBar.fillAmount = element.HP / 10;
        armorStatsBar.fillAmount = element.Armor / 10;
    }

}
