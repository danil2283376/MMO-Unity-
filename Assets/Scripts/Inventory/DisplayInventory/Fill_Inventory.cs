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
        Debug.Log(countInventorySlots);
        for (int i = 0; i < countInventorySlots; i++)
        {
            InventorySlot inventorySlot = panel.GetChild(i).GetComponent<InventorySlot>();
            //inventorySlot.itemObject = new ItemObject();
            //inventorySlot.itemObject.typeItem = TypeItem.Default;
            �������� ������ ������ ����� ���������,
            �� ����� ������ ��������� � ���� ��������
            ��� �������� ��������
            inventory.AddItem(inventorySlot.itemObject, 0);
            //Debug.Log(inventory.inventory.Count);
        }
    }
}
