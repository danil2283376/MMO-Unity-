using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPlayer : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject fastSlotsInventory;

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

        if (Physics.Raycast(originRay, out raycastHit, 1f))
        {
            Item item = raycastHit.transform.GetComponent<Item>();
            if (item != null)
            {
                PickUpItem(item);
                Destroy(raycastHit.transform.gameObject);
            }
        }
    }

    private void PickUpItem(Item item)
    {
        fastSlotsInventory.CheckInventoryFull(item.item);
        if (fastSlotsInventory.InventoryIsFull == true)
            inventory.AddItemInSlot(item.item, item.amount);
        else
            fastSlotsInventory.AddItemInSlot(item.item, item.amount);
    }

    private void OnApplicationQuit()
    {
        if (inventory.inventory != null)
            inventory.inventory.Clear();
        if (fastSlotsInventory.inventory != null)
            fastSlotsInventory.inventory.Clear();
    }
}
