using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private Vector3 _startPosition;

    public List<MonoBehaviour> scriptsForDeactivate;
    public GameObject player;

    private bool _inventoryActive { get; set; } = false;

    private MouseMove _mousePlayer;
    private GameObject _rightPlayerHand;
    private MonoBehaviour[] _scriptOnUseItem;
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
        _rightPlayerHand = player.GetComponent<BonesPlayer>().rightArm.transform.GetChild(0).gameObject;
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

            ActivateScript(true);
        }
        else
        {
            gameObject.transform.localPosition = _startPosition;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            ActivateScript(false);
        }
    }

    private void ActivateScript(bool activate)
    {
        _mousePlayer.enabled = activate;
        for (int script = 0; script < scriptsForDeactivate.Count; script++)
            scriptsForDeactivate[script].enabled = activate;
        if (_rightPlayerHand.transform.childCount > 0)
        {
            _scriptOnUseItem = _rightPlayerHand.transform.GetChild(0).GetComponents<MonoBehaviour>();
            //Debug.Log(_scriptOnUseItem.Length);
            for (int script = 0; script < _scriptOnUseItem.Length; script++)
                _scriptOnUseItem[script].enabled = activate;
        }
    }
}
