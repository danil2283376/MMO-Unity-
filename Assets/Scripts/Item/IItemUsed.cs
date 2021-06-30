using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemUsed
{
    public InventorySlot inventorySlotItem { get; set; }
    public GameObject player { get; set; }

    public void UseItem();
}
