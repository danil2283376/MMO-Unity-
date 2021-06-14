using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object",
    menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public int attack;
    public int ammo;

    public void Awake()
    {
        typeItem = TypeItem.Weapon;
    }
}
