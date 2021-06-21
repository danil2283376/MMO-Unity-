using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//œÀŒ’Œ –¿¡Œ“¿≈“ ƒŒ–¿¡Œ“¿“‹, Ã≈Õﬂ≈“ œ–≈ƒÃ≈“€ “ŒÀ‹ Œ — œ”—“€Ã» ﬂ◊≈… ¿Ã»,
//“¿  ∆≈ ÀŒÃ¿≈“ ¬€¡Œ– ﬂ◊≈≈ !!!

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
                                _selectItemForChange = null;
                                _selectItemForSwap = null;
                            }
                            Debug.Log(_selectItemForSwap);
                        }
                    }
                }
            }
            _keyIsDown = false;
        }
    }
    // ƒÓ·‡‚ËÚ¸ ÂÒÎË ÚËÔ˚ Ó‰ËÌ‡ÍÓ‚˚Â ÚÓ ‰Ó·‡‚ÎˇÚ¸ ÔÓÒÚÓ Í ÍÓÎ-‚Û
    private void SwapItemsInInventory(ref InventorySlot inventoryItem1, ref InventorySlot inventoryItem2)
    {
        InventorySlot tempSlot = new InventorySlot();
        tempSlot.Clone(inventoryItem1);

        inventoryItem1.SetValueInSlot(inventoryItem2.ItemObjectInSlot, inventoryItem2.Amount);
        //GameObject gameObjectSlot1 = inventoryItem1.gameObject;
        //GameObject gameObjectSlot2 = inventoryItem2.gameObject;

        //Destroy(gameObjectSlot1.GetComponent<InventorySlot>());
        //gameObjectSlot1.AddComponent<InventorySlot>();
        //gameObjectSlot1.GetComponent<InventorySlot>().Clone(inventoryItem2);

        //Destroy(gameObjectSlot2.GetComponent<InventorySlot>());
        //gameObjectSlot2.AddComponent<InventorySlot>();
        //gameObjectSlot2.GetComponent<InventorySlot>().Clone(tempSlot);
        //tempSlot.Clone(inventoryItem1);//InventorySlot tmpSlot = inventoryItem1;
        //inventoryItem1.Clone(inventoryItem2);
        inventoryItem2.SetValueInSlot(tempSlot.ItemObjectInSlot, tempSlot.Amount);
        //inventoryItem2.Clone(tempSlot);
    }
}
