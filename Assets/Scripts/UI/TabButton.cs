﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Image))]
public class TabButton : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TabGroup tabGroup;
    [HideInInspector]public Image background;
    public ItemType tabType;

    private void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}
