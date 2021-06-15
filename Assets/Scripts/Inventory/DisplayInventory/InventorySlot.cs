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
    public GameObject gameObjectSlot;

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

    public InventorySlot(InventorySlot copy)
    {
        itemObject = copy.itemObject;
        amount = copy.amount;
        gameObjectSlot = copy.gameObjectSlot;
        _imageOnSlot = copy._imageOnSlot;
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
            if (amount <= 0)
                amount = _amount;
            else
                AddAmount(_amount);
            UpdateInventorySlot();
        }
    }

    private void UpdateInventorySlot()
    {
        Sprite sprite = itemObject.sprite;
        //Debug.Log(_imageOnSlot.sprite);
        //if (_imageOnSlot.sprite == null)
        //    Debug.Log("_imageOnSlot.sprite == null");
        //if (itemObject.sprite == null)
        //    Debug.Log("itemObject.sprite == null");
        //if (_imageOnSlot.sprite != null && itemObject.sprite != null)
        _imageOnSlot = gameObjectSlot.GetComponent<Image>();//GetComponent<Image>();
        _imageOnSlot.sprite = itemObject.sprite;
    }
}
