using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEquipment : MonoBehaviour
{
    [Header ("Item References")]
    [SerializeField] private Transform Front;
    [SerializeField] private Transform Topper;
    [SerializeField] private Transform Antena;
    [Header ("Color")]
    [SerializeField] private Texture[] colorTextures;

    private SlotGroup[] slotGroups;
    private Renderer rend;
    private List<CarEquipmentData> actualEquipment;
    private Dictionary<ItemType, GameObject> cloneItems;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        slotGroups = Resources.FindObjectsOfTypeAll<SlotGroup>();
        foreach(SlotGroup slotGroup in slotGroups)
        {
            slotGroup.OnSlotSelectedTrigger += EquipFeature;
        }
    }

    private void OnDisable()
    {
        slotGroups = Resources.FindObjectsOfTypeAll<SlotGroup>();
        foreach (SlotGroup slotGroup in slotGroups)
        {
            slotGroup.OnSlotSelectedTrigger -= EquipFeature;
        }
    }

    private void EquipFeature(CarEquipmentData carEquipmentData)
    {
        switch (carEquipmentData.itemType)
        {
            case ItemType.Front:
                AddItem(carEquipmentData, Front);
                SaveEquipment(carEquipmentData);
                break;
            case ItemType.Topper:
                AddItem(carEquipmentData, Topper);
                SaveEquipment(carEquipmentData);
                break;
            case ItemType.Antena:
                AddItem(carEquipmentData, Antena);
                SaveEquipment(carEquipmentData);
                break;
            case ItemType.Color:
                ChangeColor(carEquipmentData.name);
                SaveEquipment(carEquipmentData);
                break;
            default:
                break;
        }
    }

    private void AddItem(CarEquipmentData item, Transform itemTransform)
    {
        if (item.equpmentPrefab != null)
        {
            // Check if the Dictionary init
            if (cloneItems == null) cloneItems = new Dictionary<ItemType, GameObject>();

            if (cloneItems.ContainsKey(item.itemType))
            {
                // Delete the actual GameObject and Add the new GameObject
                Destroy(cloneItems[item.itemType]);
                cloneItems.Remove(item.itemType);
            }

            cloneItems.Add(item.itemType, Instantiate(item.equpmentPrefab, itemTransform.position, itemTransform.rotation, itemTransform));
        }
        else
        {
            Debug.LogError("(CarEquipment) Can't found the item prefab");
        }      
    }

    private void RemoveItem(GameObject item)
    {

    }

    private void ChangeColor(string textureName)
    {
        int newColorIndex = -1;
        // Handle errors
        if (textureName == null || textureName == "")
        {
            Debug.LogError("(CarEquipment) Could't be found a color name");
            return;
        }

        switch (textureName)
        {
            case "Red":
                newColorIndex = 0;
                break;
            case "Green":
                newColorIndex = 1;
                break;
            case "Blue":
                newColorIndex = 2;
                break;
            case "Yellow":
                newColorIndex = 3;
                break;
            default:
                break;
        }

        if(newColorIndex == -1)
        {
            Debug.LogError("(CarEquipment) Could't be found the actual color name in the color list");
            return;
        }else if (newColorIndex >= colorTextures.Length)
        {
            Debug.LogError("(CarEquipment) Color index out of range");
            return;
        }
        rend.material.SetTexture("_BaseMap", colorTextures[newColorIndex]);
    }

    private void SaveEquipment(CarEquipmentData carEquipmentData)
    {
        bool equipReplaced = false;

        if (actualEquipment == null) actualEquipment = new List<CarEquipmentData>();

        for (int i = 0; i< actualEquipment.Count;i++)
        {
            if (actualEquipment[i].itemType == carEquipmentData.itemType)
            {
                actualEquipment[i] = carEquipmentData;
                equipReplaced = true;
            }
        }

        if (!equipReplaced)
        {
            actualEquipment.Add(carEquipmentData);
        }
    }
}
