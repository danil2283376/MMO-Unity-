using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventorySlot))]
public class ActivateSlotOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventorySlot _inventorySlot;

    private void Start()
    {
        this._inventorySlot = GetComponent<InventorySlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventorySlot.ActivateSlot(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventorySlot.ActivateSlot(false);
    }
}
