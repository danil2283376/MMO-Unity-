using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPlayer : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fastSlotsInventory;
    public float distanceRayPickUpItem = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickItem();
    }

    private void PickItem() 
    {
        RaycastHit raycastHit;
        Vector3 directionRay = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray originRay = Camera.main.ScreenPointToRay(directionRay);

        if (Physics.Raycast(originRay, out raycastHit, distanceRayPickUpItem))
        {
            Item item = raycastHit.transform.GetComponent<Item>();
            if (item != null)
            {
                IStorageItem storageItem = raycastHit.transform.GetComponent<IStorageItem>();
                if (storageItem != null)
                {
                    PickUpItem(item, storageItem);
                    Destroy(raycastHit.transform.gameObject);
                }
                else
                    throw new InvalidOperationException("On item not storage item, fix them!!!");
            }
        }
    }

    private void PickUpItem(Item item, IStorageItem storageItem)
    {
        fastSlotsInventory.CheckInventoryFull(item.item);
        if (fastSlotsInventory.InventoryIsFull == true)
            inventory.AddItemInSlot(item.item, item.countCopy, storageItem);
        else
            fastSlotsInventory.AddItemInSlot(item.item, item.countCopy, storageItem);
    }

    private void OnApplicationQuit()
    {
        if (inventory.inventory != null)
            inventory.inventory.Clear();
        if (fastSlotsInventory.inventory != null)
            fastSlotsInventory.inventory.Clear();
    }
}
