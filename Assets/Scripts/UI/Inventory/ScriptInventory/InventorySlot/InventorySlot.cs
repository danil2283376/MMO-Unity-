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
    public GameObject player;

    [HideInInspector] public int maxAmount;
    [HideInInspector] public int numberInventorySlot;
    public bool _slotIsActive { get; set; } = false;
    
    private int _amount { get; set; } = 0;
    private bool _slotIsFull { get; set; } = false;
    private ItemObject _itemObjectInSlot { get; set; } = null;
    private GameObject _gameObjectSlot { get; set; } = null;
    private TextsOnSlot _textsOnSlot { get; set; } = null;
    private ImagesOnSlot _imagesOnslot { get; set; } = null;
    private EquipmentItem _equipItem { get; set; } = null;
    private IStorageItem _storageItem { get; set; } = null;
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
            return (this._amount);
        }
        private set
        {
            if ((_amount + value) >= maxAmount)
                SlotIsFull = true;
            else
                SlotIsFull = false;
            if ((_amount + value) <= maxAmount)
            {
                this._amount = value;
                if (this._amount <= 0)
                    this._amount = 0;
                if (_textsOnSlot != null)
                    UpdateTextInventorySlot();
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
            else
                throw new InvalidOperationException("GameObjectSlot NULL!!!");
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

    public EquipmentItem EquipItem
    {
        get 
        {
            return (this._equipItem);
        }
        private set 
        {
            this._equipItem = value;
        }
    }

    public IStorageItem StorageItem 
    {
        get 
        {
            return (this._storageItem);
        }
        private set => this._storageItem = value;
    }

    private void Awake()
    {
        this.GameObjectSlot = gameObject;
        this._slotIsFull = false;
        this._textsOnSlot = new TextsOnSlot(this.GameObjectSlot);
        this._imagesOnslot = new ImagesOnSlot(this.GameObjectSlot, idleColor, activeColor);
        if (this._imagesOnslot.borderInSlot != null)
            this._imagesOnslot.borderInSlot.color = idleColor;
        if (_gameObjectSlot.tag != "InventorySlot" && _gameObjectSlot.name != "Canvas")
        {
            gameObject.AddComponent<EquipmentItem>();
            this._equipItem = gameObject.GetComponent<EquipmentItem>();
        }
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
        this._slotIsFull = copy.SlotIsFull;
        this._textsOnSlot = copy.TextsOnSlot;
        this._imagesOnslot = copy.ImagesOnSlot;
        this.numberInventorySlot = copy.numberInventorySlot;
        this.SlotIsFull = copy.SlotIsFull;
        this.idleColor = copy.idleColor;
        this.activeColor = copy.activeColor;
        this.StorageItem = copy.StorageItem;
        UpdateImageSlot();
    }

    public void AddAmount(int amount)
    {
        if (amount < 0)
            throw new InvalidOperationException("Amount not should be negative number!!!");
        this.Amount += amount;
    }

    public void SubstractAmount(int amount)
    {
        if ((this._amount - amount) < 0)
            throw new InvalidOperationException("Amount not should be negative number!!!");
        if (this.Amount - amount <= 0)
            SetDefaultValueInSlot();
        this.Amount -= amount;
        Debug.Log(this.Amount);
        if (this.Amount <= 0)
        {
            GameObject rightHandPlayer = player.GetComponent<BonesPlayer>().rightArm.transform.GetChild(0).gameObject;
            if (rightHandPlayer.transform.childCount > 0)
                Destroy(rightHandPlayer.transform.GetChild(0).gameObject);
        }
    }

    public void SetValueInSlot(ItemObject itemObject, int amount, IStorageItem storageItem)
    {
        if (itemObject != null)
        {
            this._itemObjectInSlot = itemObject;
            this._itemObjectInSlot.sprite = itemObject.sprite;
            this._itemObjectInSlot.typeItem = itemObject.typeItem;
            this.StorageItem = storageItem;
            this.Amount = amount;
            if (this._equipItem != null)
                this._equipItem.item = _itemObjectInSlot;
            if (_imagesOnslot.imageInSlot != null)
                UpdateImageSlot();
        }
        else
        {
            SetDefaultValueInSlot();
        }

        if (SlotIsActive == true)
        {
            if (EquipItem != null)
                EquipItem.EquipItem();
        }
    }


    public void ActivateSlot(bool activeSlot)
    {
        this._imagesOnslot.ActivateBorder(activeSlot);
        this.SlotIsActive = activeSlot;
        if (this._equipItem != null)
        {
            if (activeSlot == true)
                this._equipItem.EquipItem();
            else
                this._equipItem.RemoveItem();
        }
    }

    private void UpdateImageSlot()
    {
        _imagesOnslot.imageInSlot.sprite = _itemObjectInSlot.sprite;
    }

    private void UpdateTextInventorySlot()
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

    private void SetDefaultValueInSlot()
    {
        this._itemObjectInSlot = null;
        if (_imagesOnslot.imageInSlot != null)
            _imagesOnslot.imageInSlot.sprite = null;
        this._amount = 0;
        TextMeshProUGUI countItems = _textsOnSlot.SearchNeedText("CountItems");
        if (countItems != null)
            countItems.text = "";
        if (_equipItem != null)
            _equipItem.item = null;

    }
}

[RequireComponent(typeof(InventorySlot))]
public class EquipmentItem : MonoBehaviour
{
    [HideInInspector] public ItemObject item;
    [HideInInspector] public GameObject player;
    [HideInInspector] public IStorageItem storageItem;

    private bool _itemDressed = false;
    private BonesPlayer _bonesPlayer;

    private void Awake()
    {
        this.player = gameObject.GetComponent<InventorySlot>().player;
        if (this.player == null)
        {
            Debug.Log(gameObject.name);
            throw new InvalidOperationException("Player null");
        }
        _bonesPlayer = player.GetComponent<BonesPlayer>();
    }

    public void EquipItem()
    {
        if (item != null)
        {
            GameObject rightHandPlayer = _bonesPlayer.rightArm.transform.GetChild(0).gameObject;
            if (rightHandPlayer.transform.childCount > 0)
                Destroy(rightHandPlayer.transform.GetChild(0).gameObject);
            GameObject createItem = Instantiate(item.prefabItem, rightHandPlayer.transform);
            IItemUsed itemUsed = createItem.GetComponent<IItemUsed>();
            if (itemUsed != null)
            {
                SetValueInItem(itemUsed);
                if (createItem.GetComponent<Rigidbody>() != null)
                    Destroy(createItem.GetComponent<Rigidbody>());
                createItem.transform.position = rightHandPlayer.transform.position;

                _itemDressed = true;
            }
            else
            {
                Destroy(createItem);
            }
        }
    }

    public void RemoveItem()
    {
        if (item != null)
        {
            if (_itemDressed == false)
                return;
            if (_bonesPlayer.rightArm.transform.childCount > 0)
            {
                GameObject rightHandPlayer = _bonesPlayer.rightArm.transform.GetChild(0).gameObject;
                if (rightHandPlayer.transform.childCount > 0)
                    Destroy(rightHandPlayer.transform.GetChild(0).gameObject);
            }
            _itemDressed = false;
        }
    }

    private void SetValueInItem(IItemUsed itemUsed)
    {
        itemUsed.inventorySlotItem = gameObject.GetComponent<InventorySlot>();
        itemUsed.player = this.player;
    }
}