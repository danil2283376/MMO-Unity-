using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object",
    menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public int damage;
    public int accuracy;
    public int rateFire;

    public int ñartridgeInTheHorn;
    public int maxAmmo;

    public int timeReload;
    public int speedBullet;
    public GameObject prefabBullet;
    public void Awake()
    {
        typeItem = TypeItem.Weapon;
    }
}
