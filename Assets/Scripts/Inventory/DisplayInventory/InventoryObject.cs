using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object",
    menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
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
            inventory.Add(new InventorySlot(inventorySlot));
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
                        if (inventory[i].itemObject.typeItem == itemObject.typeItem)
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
                        Debug.Log("KALL");
                        inventory[i].SetValueInSlot(itemObject, amount);
                        inventoryIsFull = false;
                        break ;
                    }
                }
                // Инвентарь полный, и не найдено похожего предмета
                if (i == inventory.Count)
                    inventoryIsFull = true;
                //Debug.Log("index empty slot: " + i);
            }
            else
                inventory[indexFindItem].AddAmount(amount);
        }
    }
    //private void SetValueInSlot(ItemObject itemObject, int amount, int i)
    //{
    //    inventory[i].itemObject = itemObject;
    //    inventory[i].amount = amount;
    //    inventory[i].
    //}
}