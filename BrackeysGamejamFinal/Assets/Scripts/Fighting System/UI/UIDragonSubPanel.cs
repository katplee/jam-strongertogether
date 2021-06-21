using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragonSubPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private static UIDragonSubPanel instance;
    public static UIDragonSubPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIDragonSubPanel>();
            }
            return instance;
        }
    }

    public DragonType Type { get; private set; }
    private List<DragonData> dragonList = new List<DragonData>();

    private Animator animator;

    //Interactibility-related variables
    private RectTransform rect;
    private Image image;

    //Position-related variables
    private Vector3 mainPanelUpperLeftPt;
    private Vector3 mousePosition;

    //Dragon-listing-related variables
    private GameObject interim; //refers to the vertical container in-between this and the dragon list

    void Start()
    {
        //set panel to zero scale, but active
        rect = GetComponent<RectTransform>();
        rect.localScale = new Vector3(0f, 0f, 0f);

        //retrieve animator component
        animator = transform.parent.GetComponent<Animator>();

        //retrieve interim vertical container
        interim = transform.GetChild(0).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //animator.SetBool("mouseOn", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //animator.SetBool("mouseOn", false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private Vector3 ParentUpperLeftPoint()
    {
        //obtain the necessary points for position computation
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        BoxCollider2D box = transform.parent.GetComponent<BoxCollider2D>();
        mainPanelUpperLeftPt = new Vector3(
            box.bounds.center.x - box.bounds.extents.x,
            box.bounds.center.y + box.bounds.extents.y,
            0);

        return mainPanelUpperLeftPt;
    }

    private Vector3 MousePosition(Vector3 mousePosition)
    {
        this.mousePosition = mousePosition;

        return this.mousePosition;
    }

    public void PrepareForSubPanelGeneration()
    {
        //instantiate 
    }
    
    public void GenerateSubPanel(Vector3 mousePosition)
    {
        Vector3 subPanelUpperLeftPoint = MousePosition(mousePosition) - ParentUpperLeftPoint();
        RectTransform subPanel = GetComponent<RectTransform>();
        subPanel.anchoredPosition = subPanelUpperLeftPoint;
    }

    public void PassDragonList(List<DragonData> list)
    {
        dragonList = list;
    }

    public void SetDragonType(DragonType dragonType)
    {
        Type = dragonType;
    }

    public bool ToggleInteractability()
    {
        if (Vector3.Magnitude(rect.localScale) == 0)
        {
            rect.localScale = new Vector3(1f, 1f, 1f);

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(gameObject.activeSelf ^ true);
        }

        return gameObject.activeSelf;
    }
}
