using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeItem 
{
    Weapon,
    Food,
    Default
};

public class ItemObject : ScriptableObject
{
    public GameObject prefabItem;
    [TextArea]
    public string description;
    public TypeItem typeItem;
}
