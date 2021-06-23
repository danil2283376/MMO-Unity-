using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapItems : MonoBehaviour
{
    private InventorySlot _firstSlotForChange;
    private InventorySlot _secondSlotForChange;
    private bool _keyIsDown = false;

    Появляется проблема с тем что если я предмет с большим кол-вом,
    перетощу в слот с меньший вместительностью, происходит баг

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
                            bool mergeHappened = EqualsItemMerge(_firstSlotForChange, _secondSlotForChange);
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
            tempSlot.SetValueInSlot(inventorySlot1.ItemObjectInSlot, inventorySlot1.Amount);
            //Debug.Log("Amount: " + tempSlot.Amount);
            if (inventorySlot2.ItemObjectInSlot != null)
            {
                inventorySlot1.SetValueInSlot(inventorySlot2.ItemObjectInSlot, inventorySlot2.Amount);
                inventorySlot2.SetValueInSlot(tempSlot.ItemObjectInSlot, tempSlot.Amount);
            }
            else
            {
                //inventoryItem1.SetValueInSlot(null, 0);
                inventorySlot1.SetValueInSlot(null, 0);
                inventorySlot2.SetValueInSlot(tempSlot.ItemObjectInSlot, tempSlot.Amount);
            }
            Destroy(tempSlot);
        }
    }

    private bool EqualsItemMerge(InventorySlot inventorySlot1, InventorySlot inventorySlot2)
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
                    int freeSpaceInSlot2 = inventorySlot2.maxAmount - inventorySlot2.Amount;
                    int freeSpaceForAdd = freeSpaceInSlot2 - inventorySlot1.Amount;
                    //int result = inventorySlot2.maxAmount - itemForAdd;
                    //if (result < 0)
                    //    result = 0;
                    if (freeSpaceForAdd >= inventorySlot1.Amount)
                    {
                        //Debug.Log(result);
                        inventorySlot2.AddAmount(inventorySlot1.Amount);
                        inventorySlot1.SubstractAmount(inventorySlot1.Amount);
                        return (true);
                    }
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
