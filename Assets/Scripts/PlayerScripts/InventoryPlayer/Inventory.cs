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
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
}
