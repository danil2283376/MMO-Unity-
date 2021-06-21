using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwapItems : MonoBehaviour
{
    private InventorySlot _selectItemForChange;
    private InventorySlot _selectItemForSwap;
    private bool _keyIsDown = false;
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
                            Debug.Log(_selectItemForChange);
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
                            }
                            Debug.Log(_selectItemForSwap);
                        }
                    }
                }
            }
            _keyIsDown = false;
        }
    }

    private void SwapItemsInInventory(ref InventorySlot inventoryItem1, ref InventorySlot inventoryItem2)
    {
        InventorySlot tmpSlot = inventoryItem1;
        inventoryItem1.Clone(inventoryItem2);
        inventoryItem2.Clone(tmpSlot);
    }
}
