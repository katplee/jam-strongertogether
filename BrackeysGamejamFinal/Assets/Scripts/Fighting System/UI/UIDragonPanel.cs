using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragonPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private DragonData selectedDragon;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("mouseOn", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("mouseOn", false);
    }

    public void ParentParameters()
    {
        RectTransform rect = GetComponent<RectTransform>();
    }
}
