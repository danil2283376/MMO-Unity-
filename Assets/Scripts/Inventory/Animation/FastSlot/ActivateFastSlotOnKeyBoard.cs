using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateFastSlotOnKeyBoard : MonoBehaviour
{
    private int _lastClickButton = 0;
    private bool _activatingSameSlot = false;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    // if click last button == pressed in front
                    if (i == _lastClickButton)
                    {
                        // Check activate same slot or not
                        if (_activatingSameSlot == false)
                        {
                            ActivateSlot(_lastClickButton);
                            _activatingSameSlot = true;
                        }
                        else
                        {
                            DeactivateSlot(_lastClickButton);
                            _activatingSameSlot = false;
                        }
                    }
                    else
                    {
                        DeactivateSlot(_lastClickButton);
                        _lastClickButton = i;
                        ActivateSlot(i);
                    }
                }
            }
        }
    }

    private void ActivateSlot(int index)
    {
        GameObject gameObjectSlot = gameObject.transform.GetChild(index).gameObject;
        InventorySlot inventorySlot = gameObjectSlot.GetComponent<InventorySlot>();
        inventorySlot.ActivateSlot(true);
    }

    private void DeactivateSlot(int index)
    {
        GameObject gameObjectSlot = gameObject.transform.GetChild(index).gameObject;
        InventorySlot inventorySlot = gameObjectSlot.GetComponent<InventorySlot>();
        inventorySlot.ActivateSlot(false);
    }
}
