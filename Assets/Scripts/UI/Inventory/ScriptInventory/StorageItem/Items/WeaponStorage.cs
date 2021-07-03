using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStorage : MonoBehaviour, IStorageItem
{
    [HideInInspector] public int _currentAmmo;
    [HideInInspector] public int _currentMaxAmmo;
    public void SaveValueInObject() 
    {

    }
}