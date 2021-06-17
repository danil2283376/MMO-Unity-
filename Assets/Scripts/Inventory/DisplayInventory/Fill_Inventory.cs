using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void Awake()
    {
        inventory.inventoryIsFull = false;
        GameObject inventoryPanel = transform.gameObject;

        int countInventorySlots = inventoryPanel.transform.childCount;
        for (int i = 0; i < countInventorySlots; i++)
        {
            InventorySlot inventorySlot = inventoryPanel.transform.GetChild(i).GetComponent<InventorySlot>();
            inventorySlot.gameObjectSlot = inventorySlot.gameObject;
            inventorySlot.maxAmount = inventory.maxAmountInventorySlot;
            inventorySlot.numberInventorySlot = i;
            inventory.AddItem(inventorySlot.itemObject, 0, inventorySlot);
        }
    }
}
