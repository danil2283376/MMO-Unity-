using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapItems : MonoBehaviour
{
    private InventorySlot _selectItemForChange;
    private InventorySlot _selectItemForSwap;
    private bool _keyIsDown = false;


    // Rewrite on two fuction:
    // 1. GetKey();
    // 2. GetKeyUp();
    private void Update()
    {
        // Left button mouse pressed?
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && _keyIsDown == false)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;

                List<RaycastResult> result = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, result);

                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i].gameObject.layer == LayerMask.NameToLayer("UIInventorySlot"))
                        {
                            InventorySlot inventorySlot = result[i].gameObject.GetComponent<InventorySlot>();
                            _selectItemForChange = inventorySlot;
                            //if (_selectItemIndex )
                            _keyIsDown = true;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;

                List<RaycastResult> result = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, result);

                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i].gameObject.layer == LayerMask.NameToLayer("UIInventorySlot"))
                        {
                            InventorySlot inventorySlot = result[i].gameObject.GetComponent<InventorySlot>();
                            _selectItemForSwap = inventorySlot;
                            if (_selectItemForChange != _selectItemForSwap)
                            {
                                SwapItemsInInventory(ref _selectItemForChange, ref _selectItemForSwap);
                                _selectItemForChange = null;
                                _selectItemForSwap = null;
                            }
                        }
                    }
                }
            }
            _keyIsDown = false;
        }
    }
    // Добавить если типы одинаковые то добавлять просто к кол-ву
    private void SwapItemsInInventory(ref InventorySlot inventoryItem1, ref InventorySlot inventoryItem2)
    {
        if (inventoryItem1.ImagesOnSlot.imageInSlot.sprite != null)
        {
            gameObject.AddComponent<InventorySlot>();
            InventorySlot tempSlot = gameObject.GetComponent<InventorySlot>();
            tempSlot.SetValueInSlot(inventoryItem1.ItemObjectInSlot, inventoryItem1.Amount);
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
}
