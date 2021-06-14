using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object",
    menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventory = new List<InventorySlot>();

    public void AddItem(ItemObject itemObject, int amount) 
    {
        int indexFindItem = IndexFindItem(itemObject);
        if (indexFindItem != -1)
            inventory[indexFindItem].AddAmount(amount);
        else
            inventory.Add(new InventorySlot(itemObject, amount));
    }

    public int IndexFindItem(ItemObject itemObject)
    {
        for (int i = 0; i < inventory.Count; i++) 
        {
            if (inventory[i].itemObject.typeItem == itemObject.typeItem)
                return (i);
        }
        return (-1);
    }
}

[System.Serializable]
public class InventorySlot 
{
    // Item
    public ItemObject itemObject;
    public int amount;

    public InventorySlot(ItemObject _itemObject, int _amount)
    {
        itemObject = _itemObject;
        amount = _amount;
    }

    public void AddAmount(int _amount) 
    {
        amount += _amount;
    }
}