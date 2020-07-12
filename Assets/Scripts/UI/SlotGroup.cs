using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGroup : MonoBehaviour
{
    //Events
    public event Action<CarEquipmentData> OnSlotSelectedTrigger;

    [Header("Slots")]
    [SerializeField] private List<SlotButton> slotButtons;
    [SerializeField] private Sprite slotIdle;
    [SerializeField] private Sprite slotActive;
    
    private SlotButton selectedSlot;

    public void Subscribe(SlotButton button)
    {
        if (slotButtons == null) slotButtons = new List<SlotButton>();
        slotButtons.Add(button);
    }

    public void OnSlotsEnter(SlotButton button)
    {
        //Change alpha
        var tempColor = button.background.color;
        tempColor.a = 0.5f;
        button.background.color = tempColor;
    }

    public void OnSlotsExit(SlotButton button)
    {
        ResetSlots();
    }

    public void OnSlotsSelected(SlotButton button)
    {
        // Check if selection is the same that actual selected object
        if (selectedSlot == button) return;

        selectedSlot = button;
        ResetSlots();
        button.background.sprite = slotActive;
        //Add Observer event
        if (OnSlotSelectedTrigger != null) OnSlotSelectedTrigger(button.carEquipmentData);
    }

    private void ResetSlots()
    {
        foreach (SlotButton button in slotButtons)
        {
            if (selectedSlot != null && selectedSlot == button) continue;
            button.background.sprite = slotIdle;
            //Change alpha
            var tempColor = button.background.color;
            tempColor.a = 1f;
            button.background.color = tempColor;
        }
    }
}
