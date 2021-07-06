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
                            bool mergeHappened = MergeEqualsItem(_firstSlotForChange, _secondSlotForChange);
                            if (mergeHappened == false)
                                SwapItemsInInventory(_firstSlotForChange, _secondSlotForChange);
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
    private void SwapItemsInInventory(InventorySlot inventorySlot1, InventorySlot inventorySlot2)
    {
        if (inventorySlot1.ImagesOnSlot.imageInSlot.sprite != null)
        {
            gameObject.AddComponent<InventorySlot>();
            InventorySlot tempSlot = gameObject.GetComponent<InventorySlot>();

            tempSlot.maxAmount = inventorySlot1.maxAmount;
            tempSlot.SetValueInSlot(inventorySlot1.id, inventorySlot1.ItemObjectInSlot,
                    inventorySlot1.Amount, inventorySlot1.StorageItem);
            if (inventorySlot2.ItemObjectInSlot != null)
            {
                inventorySlot1.SetValueInSlot(inventorySlot2.id, inventorySlot2.ItemObjectInSlot, inventorySlot2.Amount,
                        inventorySlot2.StorageItem);
                inventorySlot2.SetValueInSlot(tempSlot.id, tempSlot.ItemObjectInSlot, tempSlot.Amount, 
                    tempSlot.StorageItem);
                //Debug.Log(inventorySlot1.ItemObjectInSlot.description);
            }
            else
            {
                inventorySlot1.SetValueInSlot(-1, null, 0, null);
                inventorySlot2.SetValueInSlot(tempSlot.id, tempSlot.ItemObjectInSlot, tempSlot.Amount,
                    tempSlot.StorageItem);
                //Debug.Log(inventorySlot1.ItemObjectInSlot.description);
            }
            Destroy(tempSlot);
        }
    }

    private bool MergeEqualsItem(InventorySlot inventorySlot1, InventorySlot inventorySlot2)
    {
        ItemObject itemSlot1 = inventorySlot1.ItemObjectInSlot;
        ItemObject itemSlot2 = inventorySlot2.ItemObjectInSlot;

        if (itemSlot1 != null)
        {
            if (itemSlot2 != null)
            {
                if (itemSlot1.typeItem == itemSlot2.typeItem
                    && itemSlot1.name == itemSlot2.name)
                {
                    for (int i = inventorySlot2.Amount; i < inventorySlot2.maxAmount; i++)
                    {
                        if (inventorySlot1.Amount <= 0)
                            break;
                        inventorySlot1.SubstractAmount(1);
                        inventorySlot2.AddAmount(1);
                    }
                    if (inventorySlot1.Amount <= 0)
                        inventorySlot1.SetValueInSlot(-1, null, 0, null);
                    return (true);
                }
            }
        }
        return (false);
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
