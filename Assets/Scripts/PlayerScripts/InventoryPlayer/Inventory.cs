using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                //inventory.AddItem(item.item, 1);
                inventory.AddItemInSlot(item.item, item.amount);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (inventory.inventory != null)
            inventory.inventory.Clear();
    }
}
