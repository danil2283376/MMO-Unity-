using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapItems : MonoBehaviour
{
    private InventorySlot _firstSlotForChange;
    private InventorySlot _secondSlotForChange;
    private bool _keyIsDown = false;

    private void Update()
    {
        // Left button mouse pressed?
        if (Input.GetKey(KeyCode.Mouse0))
            SelectFirstItem();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            SelectSecondItem();
    }

    private void SelectFirstItem()
    {
        if (EventSystem.current.IsPointerOverGameObject() && _keyIsDown == false)
        {
            List<RaycastResult> result = ReturnAllRayCastObject();
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].gameObject.layer == LayerMask.NameToLayer("UIInventorySlot"))
                    {
                        InventorySlot inventorySlot = result[i].gameObject.GetComponent<InventorySlot>();
                        _firstSlotForChange = inventorySlot;
                        _keyIsDown = true;
                    }
                }
            }
        }
    }

    private void SelectSecondItem()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            List<RaycastResult> result = ReturnAllRayCastObject();
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].gameObject.layer == LayerMask.NameToLayer("UIInventorySlot"))
                    {
                        InventorySlot inventorySlot = result[i].gameObject.GetComponent<InventorySlot>();
                        _secondSlotForChange = inventorySlot;
                        if (_firstSlotForChange != _secondSlotForChange)
                        {
                            SwapItemsInInventory(ref _firstSlotForChange, ref _secondSlotForChange);
                            _firstSlotForChange = null;
                            _secondSlotForChange = null;
                        }
                    }
                }
            }
        }
        _keyIsDown = false;
    }

    // Добавить если типы одинаковые то добавлять просто к кол-ву
    private void SwapItemsInInventory(ref InventorySlot inventoryItem1, ref InventorySlot inventoryItem2)
    {
        if (inventoryItem1.ImagesOnSlot.imageInSlot.sprite != null)
        {
            gameObject.AddComponent<InventorySlot>();
            InventorySlot tempSlot = gameObject.GetComponent<InventorySlot>();
            Debug.Log("HI");
            tempSlot.maxAmount = inventoryItem1.maxAmount;
            tempSlot.SetValueInSlot(inventoryItem1.ItemObjectInSlot, inventoryItem1.Amount);
            Debug.Log("HI");
            //Debug.Log("Amount: " + tempSlot.Amount);
            if (inventoryItem2.ItemObjectInSlot != null)
            {
                inventoryItem1.SetValueInSlot(inventoryItem2.ItemObjectInSlot, inventoryItem2.Amount);
                inventoryItem2.SetValueInSlot(tempSlot.ItemObjectInSlot, tempSlot.Amount);
            }
            else
            {
                //inventoryItem1.SetValueInSlot(null, 0);
                inventoryItem1.SetValueInSlot(null, 0);
                inventoryItem2.SetValueInSlot(tempSlot.ItemObjectInSlot, tempSlot.Amount);
            }
            Destroy(tempSlot);
        }
    }

    private List<RaycastResult> ReturnAllRayCastObject()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, result);

        return (result);
    }
}
