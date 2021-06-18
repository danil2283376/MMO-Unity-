using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fastSlotsInventory;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                PickUpItem(item);
                Destroy(other.gameObject);
            }
        }
    }

    private void PickUpItem(Item item)
    {
        fastSlotsInventory.CheckInventoryFull(item.item);
        if (fastSlotsInventory.inventoryIsFull == true)
            inventory.AddItemInSlot(item.item, item.amount);
        else
            fastSlotsInventory.AddItemInSlot(item.item, item.amount);
    }

    private void OnApplicationQuit()
    {
        if (inventory.inventory != null)
            inventory.inventory.Clear();
        if (fastSlotsInventory.inventory != null)
            fastSlotsInventory.inventory.Clear();
    }
}
