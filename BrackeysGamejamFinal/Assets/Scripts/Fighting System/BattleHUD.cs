using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text elemName; //convert to private
    public TMP_Text elemLevel; //convert to private
    public Image elemHP; //convert to private
    public Image elemArmor; //convert to private

    public void UpdateHUD<T>(T element)
        where T : Element
    {
        elemName.text = element.Type.ToString();
        //playerLevelText.text = "Lvl " + element.armor.ToString();
        //Debug.Log(element.hp / 100);
        //hpStatsBar.fillAmount = element.hp / element.maxHP;
        //armorStatsBar.fillAmount = element.armor / element.maxArmor;
    }

    public void UpdateHPArmor<T>(float hp, float armor, float maxHP, float maxArmor)
        where T : Element
    {
        //hpStatsBar.fillAmount = hp / maxHP;
        //armorStatsBar.fillAmount = armor / maxArmor;
    }

    private void SetName(TMP_Text name)
    {
        if (name == null) { throw new HUDElementInvalidException(); }

        elemName = name;
    }

    private void SetLevel(TMP_Text level)
    {
        if (level == null) { throw new HUDElementInvalidException(); }

        elemLevel = level;
    }

    private void SetHP(Image hp)
    {
        if (hp == null) { throw new HUDElementInvalidException(); }

        elemHP = hp;
    }

    private void SetArmor(Image armor)
    {
        if (armor == null) { throw new HUDElementInvalidException(); }

        elemArmor = armor;
    }

    //to be called from the UI object being declared
    public void DeclareThis<T>(string element, T UIobject)
        where T : MonoBehaviour
    {
        switch (element)
        {
            case "UILevel":
                SetLevel(UIobject as TMP_Text);
                break;

            case "UIName":
                SetName(UIobject as TMP_Text);
                break;

            case "UIHP":
                SetHP(UIobject as Image);
                break;

            case "UIArmor":
                SetArmor(UIobject as Image);
                break;
        }
    }
}
