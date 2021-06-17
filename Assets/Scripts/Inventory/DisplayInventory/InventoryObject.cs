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
        //int indexFindItem = IndexFindItem(itemObject);
        //if (indexFindItem != -1)
        //    inventory[indexFindItem].AddAmount(amount);
        //else
        //{
        //if (inventorySlot != null && amount > 0)
        //{
            inventorySlot.itemObject = itemObject;
            inventorySlot.amount = amount;

            InventorySlot inventorySlot1 = new InventorySlot(inventorySlot);
            inventory.Add(inventorySlot1);
        //}
        //}
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
            // Åñëè ìû íå íàøëè ïîõîæåãî ïðåäìåòà
            if (indexFindItem == -1)
            {
                // Èùó ïóñòîå ìåñòî ïîä ïðåäìåò
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
                // Èíâåíòàðü ïîëíûé, è íå íàéäåíî ïîõîæåãî ïðåäìåòà

                Debug.Log("i = " + i);
                //Debug.Log("inventoryIsFull: " + inventory[i].slotIsFull);
                ÊÀÊÈÌ ÒÎ ÎÁÐÀÇÎÌ i ÂÛÕÎÄÈÒ ÇÀ ÏÐÅÄÅËÛ ÌÀÑÑÈÂÀ,
                ÍÓÆÍÎ ×ÒÎÁÛ ÏÐÎÂÅÐßËÑß ÈÍÂÅÍÒÀÐÜ ÍÀ ÇÀÏÎËÍÅÍÍÎÑÒÜ ÏÓÒÅÌ,
                ÅÑËÈ ß×ÅÉÊÀ Ñ ÒÀÊÈÌ ÆÅ ÒÈÏÎÌ ÇÀÏÎËÍÅÍÀ È Â ÈÍÂÅÍÒÀÐÅ ÍÅÒ ÌÅÑÒÀ ×ÒÎÁÛ ÏÎËÎÆÈÒÜ ÒÀÊÎÉ ÆÅ ÒÈÏ ÍÎ Â ÍÎÂÓÞ ß×ÅÉÊÓ.
                ÒÎ ÈÍÂÅÍÒÀÐÜ ÇÀÏÎËÍÅÍ -> ëîãè÷íî
                if (i == (inventory.Count - 1) && inventory[i].slotIsFull == true)
                    inventoryIsFull = true;
                //Debug.Log("index empty slot: " + i);
            }
            else
                inventory[indexFindItem].AddAmount(amount);
        }
    }
}