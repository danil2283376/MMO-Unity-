using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object",
    menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public int maxAmountInventorySlot;
    public List<InventorySlot> inventory = new List<InventorySlot>();

    [HideInInspector]
    public bool inventoryIsFull = false;

    public void AddItem(InventorySlot inventorySlot) 
    {
        InventorySlot inventorySlot1 = new InventorySlot(inventorySlot);
        inventory.Add(inventorySlot1);
    }

    public int IndexFindItem(ItemObject itemObject)
    {
        if (itemObject != null)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ItemObjectInSlot != null)
                {
                    if (itemObject.typeItem != TypeItem.Default)
                        if (inventory[i].ItemObjectInSlot.typeItem == itemObject.typeItem
                            && inventory[i].ItemObjectInSlot.name == itemObject.name)
                            return (i);
                }
            }
        }
        return (-1);
    }

    public void AddItemInSlot(ItemObject itemObject, int amount)
    {
        if (itemObject != null)
        {
            int indexFindItem = IndexFindItem(itemObject);
            // Если мы не нашли похожего предмета
            if (indexFindItem == -1)
            {
                // Ищу пустое место под предмет
                int i;
                for (i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].ItemObjectInSlot == null)
                    {
                        inventory[i].SetValueInSlot(itemObject, amount);
                        //inventoryIsFull = false;
                        break;
                    }
                }
                CheckInventoryFull(itemObject);
            }
            else
                inventory[indexFindItem].AddAmount(amount);
        }
    }

    public void CheckInventoryFull(ItemObject itemObject)
    {
        int i = 0;
        if (itemObject != null)
        {
            for (i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ItemObjectInSlot == null)
                    break;
                if (inventory[i].ItemObjectInSlot.typeItem == itemObject.typeItem)
                {
                    if (inventory[i].ItemObjectInSlot.name == itemObject.name)
                    {
                        if (inventory[i].SlotIsFull == true)
                            continue;
                        else
                            break;
                    }
                }
            }
        }
        if (i == inventory.Count)
            inventoryIsFull = true;
        else
            inventoryIsFull = false;
    }
}