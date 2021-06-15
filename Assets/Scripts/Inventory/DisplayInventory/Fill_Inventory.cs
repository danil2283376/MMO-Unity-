using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void Awake()
    {
        GameObject inventoryPanel = GameObject.Find("InventoryPanel");

        Transform panel = inventoryPanel.transform.GetChild(0);

        int countInventorySlots = panel.childCount;
        for (int i = 0; i < countInventorySlots; i++)
        {
            InventorySlot inventorySlot = panel.GetChild(i).GetComponent<InventorySlot>();
            //inventory.inventory[i] = inventorySlot;
            inventorySlot.gameObjectSlot = inventorySlot.gameObject;
            //inventorySlot.itemObject = new ItemObject();
            //inventorySlot.itemObject.typeItem = TypeItem.Default;
            //�������� ������ ������ ����� ���������,
            //�� ����� ������ ��������� � ���� ��������
            //��� �������� ��������
            inventory.AddItem(inventorySlot.itemObject, 0, inventorySlot);
            //Debug.Log(inventory.inventory.Count);
        }
    }
}
