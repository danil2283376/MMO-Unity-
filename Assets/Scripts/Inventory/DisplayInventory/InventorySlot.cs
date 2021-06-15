using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public int maxAmount;

    // Item
    [HideInInspector]
    public ItemObject itemObject;
    [HideInInspector]
    public int amount;

    private Image _imageOnSlot;
    private void Awake()
    {
        _imageOnSlot = GetComponent<Image>();
    }
    public InventorySlot() 
    {
        amount = 0;
        itemObject = null;
    }

    public InventorySlot(ItemObject _itemObject, int _amount)
    {
        itemObject = _itemObject;
        amount = _amount;
    }

    public void AddAmount(int _amount)
    {
        amount += _amount;
    }

    public void SetValueInSlot(ItemObject _itemObject, int _amount)
    {
        if (_itemObject != null)
        { 
            itemObject = _itemObject;
            itemObject.sprite = _itemObject.sprite;
            itemObject.typeItem = _itemObject.typeItem;
            if (amount == 0)
                amount = _amount;
            else
                AddAmount(_amount);
        }
    }
}
