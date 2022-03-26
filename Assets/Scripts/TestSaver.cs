using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaver : MonoBehaviour
{
    public InventoryObject inventoryPlayer;
    public InventoryObject fastSlotsPlayer;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            //inventoryPlayer.Save();
            //fastSlotsPlayer.Save();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            //inventoryPlayer.Load();
            //fastSlotsPlayer.Load();
        }
    }
}
