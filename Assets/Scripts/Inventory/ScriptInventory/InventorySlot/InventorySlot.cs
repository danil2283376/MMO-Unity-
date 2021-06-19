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
    [HideInInspector] public bool slotIsActive = false;
    [HideInInspector] public int numberInventorySlot;

    private int _amount { get; set; } = 0;
    private bool _slotIsFull { get; set; } = false;
    private ItemObject _itemObjectInSlot { get; set; } = null;
    private GameObject _gameObjectSlot { get; set; } = null;

    private TextsOnSlot _textsOnSlot = null;
    private ImagesOnSlot _imagesOnslot = null;

    public int Amount
    {
        get
        {
            return this._amount;
        }
        private set
        {
            if (this._amount == 0)
                this._amount = value;
            else
                this._amount += value;
            UpdateTextInventorySlot();
        }
    }
    public bool SlotIsFull
    {
        get
        {
            return (this._slotIsFull);
        }
        private set
        {
            _slotIsFull = value;
        }
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


    private void Awake()
    {
        this.GameObjectSlot = gameObject;
        this._slotIsFull = false;
        this._textsOnSlot = new TextsOnSlot(this.GameObjectSlot);
        this._imagesOnslot = new ImagesOnSlot(this.GameObjectSlot, idleColor, activeColor);
    }

    public InventorySlot(InventorySlot copy)
    {
        this.ItemObjectInSlot = copy.ItemObjectInSlot;
        this._amount = copy._amount;
        this._gameObjectSlot = copy.GameObjectSlot;
        this.maxAmount = copy.maxAmount;
        this._slotIsFull = copy._slotIsFull;
        this._textsOnSlot = copy._textsOnSlot;
        this._imagesOnslot = copy._imagesOnslot;
        this.numberInventorySlot = copy.numberInventorySlot;
        //this.SlotIsFull = false;
        this.SlotIsFull = copy.SlotIsFull;
    }

    public void Clone(InventorySlot copy)
    {
        this.ItemObjectInSlot = copy.ItemObjectInSlot;
        this._amount = copy._amount;
        this._gameObjectSlot = copy.GameObjectSlot;
        this.maxAmount = copy.maxAmount;
        this._slotIsFull = copy._slotIsFull;
        this._textsOnSlot = copy._textsOnSlot;
        this._imagesOnslot = copy._imagesOnslot;
        this.numberInventorySlot = copy.numberInventorySlot;
        //this.SlotIsFull = false;
        this.SlotIsFull = copy.SlotIsFull;
    }

    public void AddAmount(int amount)
    {
        this.Amount = amount;
    }

    public void SetValueInSlot(ItemObject itemObject, int amount)
    {
        if (itemObject != null)
        {
            this._itemObjectInSlot = itemObject;
            this._itemObjectInSlot.sprite = itemObject.sprite;
            this._itemObjectInSlot.typeItem = itemObject.typeItem;
            AddAmount(amount);
            UpdateImageSlot();
        }
    }

    public void SlotIsActive()
    {
        slotIsActive = !slotIsActive;
        _imagesOnslot.activeSlot = slotIsActive;
        _imagesOnslot.ActivateImage();
    }

    private void UpdateImageSlot()
    {
        if (_imagesOnslot.imageInSlot != null)
        _imagesOnslot.imageInSlot.sprite = _itemObjectInSlot.sprite;
    }

    private void UpdateTextInventorySlot()
    {
        if (_slotIsFull == false)
        {
            if (_itemObjectInSlot != null)
            {
                    TextMeshProUGUI countItems = _textsOnSlot.SearchNeedText("CountItems");
                if (countItems != null)
                    countItems.text = _amount + "/" + maxAmount;
            }
        }
    }
}