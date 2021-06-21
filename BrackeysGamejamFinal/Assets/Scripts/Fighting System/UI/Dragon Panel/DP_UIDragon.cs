using UnityEngine;
using UnityEngine.EventSystems;


public class DP_UIDragon : UIObject, IPointerEnterHandler, IPointerExitHandler
{
    private DP_UIAvatar dragonAvatar;
    private DP_UIName dragonName;
    private DP_UIHP dragonHP;
    private DP_UIXP dragonXP;
    private DP_UIOpacity dragonOpacity;

    //check this
    public override string Label
    {
        get { return transform.tag; }
    }

    private void SetAvatar(DP_UIAvatar avatar)
    {
        dragonAvatar = avatar;
    }

    private void SetName(DP_UIName name)
    {
        dragonName = name;
    }

    private void SetHP(DP_UIHP hp)
    {
        dragonHP = hp;
    }

    private void SetXP(DP_UIXP xp)
    {
        dragonXP = xp;
    }

    private void SetOpacity(DP_UIOpacity opacity)
    {
        dragonOpacity = opacity;
    }

    public void DeclareThis<T>(string element, T DP_UIObject)
        where T : MonoBehaviour
    {
        switch (element)
        {
            case "DP_UIAvatar":
                SetAvatar(DP_UIObject as DP_UIAvatar);
                break;

            case "DP_UIName":
                SetName(DP_UIObject as DP_UIName);
                break;

            case "DP_UIHP":
                SetHP(DP_UIObject as DP_UIHP);
                break;

            case "DP_UIXP":
                SetXP(DP_UIObject as DP_UIXP);
                break;

            case "DP_UIOpacity":
                SetOpacity(DP_UIObject as DP_UIOpacity);
                break;

            
        }
    }

    public void UpdateDragon<T>(T dragon)
        where T : Dragon
    {
        dragonAvatar.ChangeAvatar();
        dragonName.ChangeText(dragon.name);
        dragonHP.ChangeFillAmount(dragon.NormalHP());
        //dragonXP.ChangeFillAmount();
    }

    private void UpdateOpacity(bool active)
    {
        dragonOpacity.ChangeOpacity(active);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateOpacity(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpdateOpacity(true);
    }
}
