using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public Color idleColor;
    public Color activeColor;

    [HideInInspector] public int maxAmount;
    [HideInInspector] public int numberInventorySlot;

    public bool _slotIsActive { get; set; } = false;
    private int _amount { get; set; } = 0;
    private bool _slotIsFull { get; set; } = false;
    private ItemObject _itemObjectInSlot { get; set; } = null;
    private GameObject _gameObjectSlot { get; set; } = null;
    private TextsOnSlot _textsOnSlot { get; set; } = null;
    private ImagesOnSlot _imagesOnslot { get; set; } = null;

    public bool SlotIsActive
    {
        get
        {
            return (_slotIsActive);
        }
        private set => _slotIsActive = value;
    }

    public int Amount
    {
        get
        {
            return this._amount;
        }
        private set
        {
            //if (value > maxAmount)
            //    throw new InvalidOperationException("value > maxAmount, in Script InventorySlot!!!");
            if ((_amount + value) <= maxAmount)
            {
                if (this._amount == 0)
                    this._amount = value;
                else
                    this._amount += value;
                SlotIsActive = false;
                UpdateTextInventorySlot();
            }
            else
            {
                SlotIsActive = true;
            }
        }
    }

    public bool SlotIsFull
    {
        get
        {
            return (this._slotIsFull);
        }
        private set => this._slotIsFull = value;
    }

    public ItemObject ItemObjectInSlot
    {
        get
        {
            return (this._itemObjectInSlot);
        }
        private set
        {
            if (value != null)
                this._itemObjectInSlot = value;
        }
    }

    public GameObject GameObjectSlot
    {
        get
        {
            return (this._gameObjectSlot);
        }
        private set
        {
            if (value != null)
                this._gameObjectSlot = value;
        }
    }

    public TextsOnSlot TextsOnSlot
    {
        get
        {
            return (_textsOnSlot);
        }
        private set => this._textsOnSlot = value;
    }

    public ImagesOnSlot ImagesOnSlot 
    {
        get 
        {
            return (_imagesOnslot);
        }
        private set => this._imagesOnslot = value;
    }

    private void Awake()
    {
        this.GameObjectSlot = gameObject;
        this._slotIsFull = false;
        this._textsOnSlot = new TextsOnSlot(this.GameObjectSlot);
        this._imagesOnslot = new ImagesOnSlot(this.GameObjectSlot, idleColor, activeColor);
        //if (_imagesOnslot.imageInSlot == null)
        //    throw new InvalidOperationException("Image in slot not Found!!!");
        //if (_imagesOnslot.borderInSlot == null)
        //    throw new InvalidOperationException("Border in slot not Found!!!");
        if (this._imagesOnslot.borderInSlot != null)
            this._imagesOnslot.borderInSlot.color = idleColor;
    }

    public InventorySlot()
    {

    }

    public InventorySlot(InventorySlot copy)
    {
        this.TextsOnSlot = copy.TextsOnSlot;
        this.ImagesOnSlot = copy.ImagesOnSlot;
        this.Amount = copy.Amount;
    }

    public void Clone(InventorySlot copy)
    {
        if (copy == this)
            throw new InvalidOperationException("Copying yourself!!!");

        this.ItemObjectInSlot = copy.ItemObjectInSlot;
        this._gameObjectSlot = copy.GameObjectSlot;
        this.Amount = copy.Amount;
        this.maxAmount = copy.maxAmount;
        Debug.Log("maxAmount: " + maxAmount);
        this._slotIsFull = copy.SlotIsFull;
        this._textsOnSlot = copy.TextsOnSlot;
        this._imagesOnslot = copy.ImagesOnSlot;
        this.numberInventorySlot = copy.numberInventorySlot;
        this.SlotIsFull = copy.SlotIsFull;
        this.idleColor = copy.idleColor;
        this.activeColor = copy.activeColor;
        UpdateImageSlot();
    }

    public void AddAmount(int amount)
    {
        if (amount < 0)
            throw new InvalidOperationException("Amount not should be negative number!!!");
        this.Amount = amount;
    }

    public void SetValueInSlot(ItemObject itemObject, int amount)
    {
        if (itemObject != null)
        {
            this._itemObjectInSlot = itemObject;
            this._itemObjectInSlot.sprite = itemObject.sprite;
            this._itemObjectInSlot.typeItem = itemObject.typeItem;
            this._amount = amount;
            if (_textsOnSlot != null)
                UpdateTextInventorySlot();
            if (_imagesOnslot.imageInSlot != null)
                UpdateImageSlot();
        }
        else
        {
            SetDefaultValueInSlot();
        }
    }

    public void ActivateSlot(bool activeSlot)
    {
        this._imagesOnslot.ActivateBorder(activeSlot);
    }

    private void UpdateImageSlot()
    {
        _imagesOnslot.imageInSlot.sprite = _itemObjectInSlot.sprite;
    }

    private void UpdateTextInventorySlot()
    {
        if (_slotIsFull == false)
        {
            if (_itemObjectInSlot != null)
            {
                if (_textsOnSlot != null)
                {
                    TextMeshProUGUI countItems = _textsOnSlot.SearchNeedText("CountItems");
                    if (countItems != null)
                        countItems.text = _amount + "/" + maxAmount;
                }
            }
        }
    }

    private void SetDefaultValueInSlot()
    {
        this._itemObjectInSlot = null;
        if (_imagesOnslot.imageInSlot != null)
            _imagesOnslot.imageInSlot.sprite = null;
        this._amount = 0;
        TextMeshProUGUI countItems = _textsOnSlot.SearchNeedText("CountItems");
        if (countItems != null)
            countItems.text = "";
    }
}