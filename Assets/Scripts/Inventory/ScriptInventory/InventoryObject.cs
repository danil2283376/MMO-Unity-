using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object",
    menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventory = new List<InventorySlot>();
    public int maxAmountInventorySlot;

    private bool inventoryIsFull { get; set; } = false;
    public bool InventoryIsFull
    {
        get 
        {
            return (inventoryIsFull);
        }
        private set 
        {
            inventoryIsFull = value;
        }
    }

    public void AddInventorySlot(GameObject gameObjectSlot)
    {
        InventorySlot inventorySlot1 = gameObjectSlot.GetComponent<InventorySlot>();
        inventorySlot1.maxAmount = maxAmountInventorySlot;
        inventory.Add(inventorySlot1);
    }

    public InventorySlot InventoryFindItem(ItemObject itemObject)
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
                            return (inventory[i]);
                }
            }
        }
        return (null);
    }

    public void AddItemInSlot(ItemObject itemObject, int amount)
    {
        if (itemObject != null)
        {
            InventorySlot findInventorySlot = InventoryFindItem(itemObject);
            // Если мы не нашли похожего предмета
            if (findInventorySlot == null ||
                findInventorySlot.SlotIsFull == true)// == -1)
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
                findInventorySlot.AddAmount(amount);//inventory[indexFindItem].AddAmount(amount);
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