using UnityEngine;

public enum TypeItem 
{
    Weapon,
    Food,
    Default
};

public class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefabItem;
    [TextArea]
    public string description;
    public TypeItem typeItem;
}
