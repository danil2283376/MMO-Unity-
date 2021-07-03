using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak_47 : MonoBehaviour, IItemUsed
{
    public InventorySlot inventorySlotItem { get; set; }
    public GameObject player { get; set; }

    private Item _weaponItem;
    private WeaponObject _weaponObject;

    private void Start()
    {
        _weaponItem = gameObject.GetComponent<Item>();
        _weaponObject = (WeaponObject)_weaponItem.item;
    }

    public void UseItem() 
    {
        Shoot();
    }

    private void Shoot() 
    {

    }
}
