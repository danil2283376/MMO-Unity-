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

    public void AddItem(ItemObject itemObject, int amount, InventorySlot inventorySlot) 
    {
        int indexFindItem = IndexFindItem(itemObject);
        if (indexFindItem != -1)
            inventory[indexFindItem].AddAmount(amount);
        else
        {
            //Debug.Log("Добавляю новый элемент в список");
            //InventorySlot copyInventory = new InventorySlot(inventorySlot);
            inventorySlot.itemObject = itemObject;
            inventorySlot.amount = amount;

            InventorySlot inventorySlot1 = new InventorySlot(inventorySlot);
            //inventorySlot1.maxAmount = maxAmountInventorySlot;
            inventory.Add(inventorySlot1);
        }
        //Debug.Log(inventory.Count);
    }

    public int IndexFindItem(ItemObject itemObject)
    {
        if (itemObject != null)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemObject != null)
                {
                    if (itemObject.typeItem != TypeItem.Default)
                        if (inventory[i].itemObject.typeItem == itemObject.typeItem
                            && inventory[i].itemObject.name == itemObject.name)
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
                    if (inventory[i].itemObject == null)
                    {
                        inventory[i].SetValueInSlot(itemObject, amount);
                        inventoryIsFull = false;
                        break;
                    }
                }
                // Инвентарь полный, и не найдено похожего предмета
                if (i == inventory.Count)
                    inventoryIsFull = true;
                //Debug.Log("index empty slot: " + i);
            }
            else
            {
                inventory[indexFindItem].AddAmount(amount);
            }
        }
    }
}