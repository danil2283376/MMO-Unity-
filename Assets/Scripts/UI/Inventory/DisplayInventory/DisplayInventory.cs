using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private MovePlayer _playerMove;

    private bool _inventoryActive { get; set; } = false;

    private MouseMove _mousePlayer;
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

        _mousePlayer = Camera.main.GetComponent<MouseMove>();
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

            _mousePlayer.enabled = true;
            _playerMove.enabled = true;
        }
        else
        {
            gameObject.transform.localPosition = _startPosition;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            _mousePlayer.enabled = false;
            _playerMove.enabled = false;
        }
    }
}
