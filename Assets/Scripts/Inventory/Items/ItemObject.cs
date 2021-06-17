using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public enum TypeItem 
{
    Weapon,
    Food,
    Default
};

public class ItemObject : ScriptableObject
{
    public Sprite sprite;
    //���������� ������ � ����������� ������� � �������� ��� � PREFABITEM
    public GameObject prefabItem;
    [TextArea]
    public string description;
    public TypeItem typeItem;
}
