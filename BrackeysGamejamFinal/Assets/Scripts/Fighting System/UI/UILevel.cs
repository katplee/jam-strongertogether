using TMPro;

public class UILevel : UIObject
{
    private BattleHUD parent;

    private TMP_Text text;

    public override string Label
    {
        get { return GetType().Name; }
    }

    private void Start()
    {
        text = GetComponent<TMP_Text>();

        if(transform.parent.TryGetComponent(out parent))
        {
            parent.DeclareThis(Label, text);
        }        
    }

    public void ChangeText(string level)
    {
        text.text = level;
    }
}
