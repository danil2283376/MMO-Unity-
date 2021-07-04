using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public InventorySlot inventorySlotItem { get; set; }
    public GameObject player { get; set; }
    //public GameObject prefabBullet { get; set; }
    public void Shoot();
    public void Reload();
}
