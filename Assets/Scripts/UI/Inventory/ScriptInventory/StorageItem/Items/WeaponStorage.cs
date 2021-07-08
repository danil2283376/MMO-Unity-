using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStorage : MonoBehaviour, IStorageItem
{
    public int currentAmmo;
    public int currentMaxAmmo;

    public int CurrentAmmo 
    {
        get 
        {
            return (currentAmmo);
        }
        set 
        {
            if (value <= 0)
                currentAmmo = 0;
            else
                currentAmmo = value;
        }
    }

    public int CurrentMaxAmmo 
    {
        get 
        {
            return (currentMaxAmmo);
        }
        set 
        {
            if ((currentMaxAmmo - value) <= 0)
                currentMaxAmmo = 0;
            else
                currentMaxAmmo = value;
        }
    }

    public void UnpackingTheInterface(object storageItem)
    {
        Type type = storageItem.GetType();
        Type serializeInterface = type.GetInterface("IStorageItem");

        if (serializeInterface.Name == "IStorageItem")
        {
            WeaponStorage weaponStorage = (WeaponStorage)storageItem;
            this.currentAmmo = weaponStorage.currentAmmo;
            this.currentMaxAmmo = weaponStorage.currentMaxAmmo;
        }
    }
}