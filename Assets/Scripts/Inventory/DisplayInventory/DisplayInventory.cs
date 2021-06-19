using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private Vector3 _startPosition;

    private bool _inventoryActive { get; set; } = false;

    public bool InventoryActive 
    {
        get 
        {
            return (this._inventoryActive);
        }
        private set
        {
            this._inventoryActive = value;
            ShowInventory();
        }
    }

    private void Start()
    {
        gameObject.transform.localPosition = _newPosition;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryActive = !InventoryActive;
        }
        if (InventoryActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InventoryActive = !InventoryActive;
            }
        }
    }

    private void ShowInventory()
    {
        if (InventoryActive == false)
        {
            gameObject.transform.localPosition = _newPosition;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            gameObject.transform.localPosition = _startPosition;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
