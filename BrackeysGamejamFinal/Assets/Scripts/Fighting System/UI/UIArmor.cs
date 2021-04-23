using UnityEngine.UI;

public class UIArmor : UIObject
{
    private BattleHUD parent;

    private Image image;

    public override string Label
    {
        get { return GetType().Name; }
    }

    private void Start()
    {
        image = GetComponent<Image>();

        if (transform.parent.TryGetComponent(out parent))
        {
            parent.DeclareThis(Label, image);
        }
    }

    public void ChangeFillAmount(float armor, float maxArmor)
    {
        image.fillAmount = armor / maxArmor;
    }
}
