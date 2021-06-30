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
    //гюцнрнбхрэ опетюа я менаундхлшл нпсфхел х онкнфхрэ ецн б PREFABITEM
    public GameObject prefabItem;
    [TextArea]
    public string description;
    public TypeItem typeItem;
}
