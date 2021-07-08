using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public Color idleColor;
    public Color activeColor;
    public GameObject player;

    [HideInInspector] public int id = -1;
    [HideInInspector] public int maxAmount;
    [HideInInspector] public int numberInventorySlot;
    public bool _slotIsActive { get; set; } = false;

    public int _amount { get; set; } = 0;
    private bool _slotIsFull { get; set; } = false;

    public ItemObject _itemObjectInSlot;// { get; set; } = null;
    public GameObject _gameObjectSlot;// { get; set; } = null;
    public TextsOnSlot _textsOnSlot;// { get; set; } = null;
    public ImagesOnSlot _imagesOnslot;// { get; set; } = null;
    public EquipmentItem _equipItem;// { get; set; } = null;
    public object _storageItem;// { get; set; } = null;

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
        set
        {
            if (value != null)
            {
                this._itemObjectInSlot = value;
                UpdateImageSlot();
                UpdateTextInventorySlot();
            }
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

    public object StorageItem
    {
        get
        {
            return (this._storageItem);
        }
        set
        {
            this._storageItem = value;
            if (this._equipItem != null)
                this._equipItem.storageItem = value;
        }
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

    public void SetValueInSlot(int id, ItemObject itemObject, int amount, object storageItem)
    {
        if (itemObject != null)
        {
            this.id = id;
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

    public void SaveItemSlot(FileStream file, BinaryFormatter bf)
    {
        Type typeClassStorage = null;

        if (this.id != -1)
        {
            typeClassStorage = this._storageItem.GetType();
            string nameType = typeClassStorage.Name;

            string saveNameStorageItem = JsonConvert.SerializeObject(nameType);
            //string saveNameStorageItem = JsonUtility.ToJson(nameType);
            Debug.Log(saveNameStorageItem);
            bf.Serialize(file, saveNameStorageItem);
        }
        string saveItemObjectInSlot = JsonUtility.ToJson(this._itemObjectInSlot, true);
        string saveTextsOnSlot = JsonUtility.ToJson(this._textsOnSlot, true);
        string saveImagesOnslot = JsonUtility.ToJson(this._imagesOnslot, true);
        string saveEquipItem = JsonUtility.ToJson(this._equipItem, true);
        string saveStorageItem = JsonUtility.ToJson(this._storageItem, true);

        bf.Serialize(file, saveItemObjectInSlot);
        bf.Serialize(file, saveTextsOnSlot);
        bf.Serialize(file, saveImagesOnslot);
        bf.Serialize(file, saveEquipItem);
        bf.Serialize(file, saveStorageItem);
    }

    public void LoadItemSlot(FileStream file, BinaryFormatter bf)
    {
        if (this.id != -1)
        {
            string type = (string)JsonConvert.DeserializeObject(bf.Deserialize(file).ToString());
        //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), type);
            Debug.Log("type: " + type);
        }
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this._storageItem);
        //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this._itemObjectInSlot);
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this._textsOnSlot);
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this._imagesOnslot);
        //JsonUtility.FromJson(bf.Deserialize(file).ToString());
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this._equipItem);
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
        this._storageItem = null;
    }

    private void AddStorageOnSlot() 
    {
        if (this._itemObjectInSlot.typeItem == TypeItem.Weapon)
            gameObject.AddComponent<WeaponStorage>();
        if (this._itemObjectInSlot.typeItem == TypeItem.Food)
            gameObject.AddComponent<FoodStorage>();

    }
}

[RequireComponent(typeof(InventorySlot))]
public class EquipmentItem : MonoBehaviour
{
    [HideInInspector] public ItemObject item;
    [HideInInspector] public GameObject player;
    [HideInInspector] public object storageItem;

    private bool _itemDressed = false;
    private BonesPlayer _bonesPlayer;

    private void Awake()
    {
        this.player = gameObject.GetComponent<InventorySlot>().player;
        if (this.player == null)
            throw new InvalidOperationException("Player null");
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
            if (createItem == null)
                throw new InvalidOperationException("EquipItem null!!!");
            IItemUsed itemUsed = createItem.GetComponent<IItemUsed>();
            //Debug.Log(weaponStorage.currentAmmo);
            if (itemUsed != null)
            {
                SetValueInItem(itemUsed, createItem);
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

    private void SetValueInItem(IItemUsed itemUsed, GameObject createEquipItem)
    {
        itemUsed.inventorySlotItem = gameObject.GetComponent<InventorySlot>();
        itemUsed.player = this.player;
        ActivateScript activateScript = createEquipItem.GetComponent<ActivateScript>();
        if (activateScript != null)
        {
            IStorageItem storageItem1 = createEquipItem.GetComponent<IStorageItem>();
            //Debug.Log(weaponStorage.currentAmmo);
            storageItem1.UnpackingTheInterface(this.storageItem);
            activateScript.ActivateScriptsOnObject();
            //storageItem = this.storageItem;
        }
    }
}