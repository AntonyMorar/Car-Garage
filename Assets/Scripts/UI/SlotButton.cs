using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ItemType
{
    Front,
    Topper,
    Antena,
    Color
}

[System.Serializable]
public struct CarEquipmentData
{
    public string name;
    public ItemType itemType;
    public GameObject equpmentPrefab;
}

[RequireComponent(typeof(Image))]
public class SlotButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slot")]
    [SerializeField] private SlotGroup slotGroup;
    [HideInInspector] public Image background;

    [Header("Equpment Data")]
    public CarEquipmentData carEquipmentData;

    private void Start()
    {
        background = GetComponent<Image>();
        slotGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slotGroup.OnSlotsSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotGroup.OnSlotsEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotGroup.OnSlotsExit(this);
    }
}
