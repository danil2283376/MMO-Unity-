using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_Inventory : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory;

    private void Awake()
    {
        GameObject inventoryPanel = transform.gameObject;

        int countInventorySlots = inventoryPanel.transform.childCount;
        for (int i = 0; i < countInventorySlots; i++)
        {
            InventorySlot inventorySlot = inventoryPanel.transform.GetChild(i).GetComponent<InventorySlot>();
            inventorySlot.numberInventorySlot = i;
            inventory.AddItem(inventorySlot);
        }
    }
}
