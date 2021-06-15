using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public ItemObject itemObject;
    [HideInInspector]
    public int maxAmount;
    [HideInInspector]
    public int amount;
    [HideInInspector]
    public bool slotIsFull = false;

    public GameObject gameObjectSlot;

    private Image _imageOnSlot;
    public InventorySlot() 
    {
        amount = 0;
        itemObject = null;
        //UpdateTextInventorySlot();
    }

    public InventorySlot(ItemObject _itemObject, int _amount)
    {
        itemObject = _itemObject;
        amount = _amount;
        //UpdateTextInventorySlot();
    }

    public InventorySlot(InventorySlot copy)
    {
        itemObject = copy.itemObject;
        amount = copy.amount;
        gameObjectSlot = copy.gameObjectSlot;
        _imageOnSlot = copy._imageOnSlot;
        maxAmount = copy.maxAmount;
        UpdateTextInventorySlot();
    }

    public void AddAmount(int _amount)
    {
        if ((amount + _amount) > maxAmount)
            slotIsFull = true;
        if (slotIsFull == false)
        {
            amount += _amount;
            UpdateTextInventorySlot();
        }
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
        _imageOnSlot = gameObjectSlot.GetComponent<Image>();
        _imageOnSlot.sprite = itemObject.sprite;
        UpdateTextInventorySlot();
    }

    private void UpdateTextInventorySlot()
    {
        if (slotIsFull == false)
        {
            TextMeshProUGUI textMeshProUGUI = gameObjectSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.text = amount + "/" + maxAmount;
        }
    }
}
