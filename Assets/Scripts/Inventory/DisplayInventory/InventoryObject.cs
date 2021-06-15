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

    public void AddItem(ItemObject itemObject, int amount) 
    {
        int indexFindItem = IndexFindItem(itemObject);
        if (indexFindItem != -1)
            inventory[indexFindItem].AddAmount(amount);
        else
        {
            //Debug.Log("�������� ����� ������� � ������");
            inventory.Add(new InventorySlot(itemObject, amount));
        }
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
            //for (i = 0; i < inventory.Count; i++)
            //{
            //    if (inventory[i].itemObject.typeItem == itemObject.typeItem)
            //    {
            //        break ;
            //    }
            //}
            // ���� �� �� ����� �������� ��������
            if (indexFindItem == -1)
            {
                //Debug.Log("��� ��������� ��������!");
                // ��� ������ ����� ��� �������
                int i;
                Debug.Log("���-�� ���������: " + inventory.Count);
                for (i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].itemObject == null)
                    {
                        //inventory[i].itemObject = new ItemObject();
                        inventory[i].SetValueInSlot(itemObject, amount);
                        inventoryIsFull = false;
                        break;
                    }
                }
                // ��������� ������, � �� ������� �������� ��������
                if (i == inventory.Count)
                    inventoryIsFull = true;
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