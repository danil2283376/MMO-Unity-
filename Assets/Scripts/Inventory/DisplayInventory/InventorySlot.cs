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

    [HideInInspector]
    public int maxAmount;
    [HideInInspector]
    public bool slotIsActive = false;
    [HideInInspector]
    public int numberInventorySlot;

    private int _amount { get; set; } = 0;
    private bool _slotIsFull { get; set; } = false;
    private ItemObject _itemObjectInSlot { get; set; } = null;
    private GameObject _gameObjectSlot { get; set; }

    private TextsOnSlot _textsOnSlot;
    private ImagesOnSlot _imagesOnslot;

    public int Amount
    {
        get
        {
            return _amount;
        }
        private set
        {
            if (_amount == 0)
                _amount = value;
            else
                _amount += value;
            UpdateTextInventorySlot();
        }
    }
    public bool SlotIsFull
    {
        get
        {
            return (_slotIsFull);
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
            return (_gameObjectSlot);
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
        this.SlotIsFull = false;
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

class ImagesOnSlot
{
    public GameObject gameObject;
    public Image imageInSlot;
    public Image borderInSlot;
    public Color idleImage;
    public Color activeImage;
    public bool activeSlot;
    public ImagesOnSlot(GameObject inventorySlot, Color _idleImage, Color _activeImage)
    {
        gameObject = inventorySlot;
        idleImage = _idleImage;
        activeImage = _activeImage;
        SearchNeedImage("ImageSlot", ref imageInSlot);
        SearchNeedImage("BorderSlot", ref borderInSlot);
    }

    public void ActivateImage()
    {
        if (activeSlot == true)
            borderInSlot.color = activeImage;
        else
            borderInSlot.color = idleImage;
    }

    private void SearchNeedImage(string nameImage, ref Image imageForWriteFindImage)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (child.name == nameImage)
            {
                imageForWriteFindImage = child.gameObject.GetComponent<Image>();
                break;
            }
        }
    }
}

class TextsOnSlot
{
    public string nameText;
    public GameObject gameObject;
    private TextMeshProUGUI[] textsOnGameObject;

    public TextsOnSlot(GameObject inventorySlot)
    {
        gameObject = inventorySlot;
        textsOnGameObject = new TextMeshProUGUI[inventorySlot.transform.childCount];
        AddAllTextsOnGameObject();
    }

    public TextMeshProUGUI SearchNeedText(string nameText)
    {
        TextMeshProUGUI findNeedText = null;
        for (int i = 0; i < textsOnGameObject.Length; i++)
        {
            if (textsOnGameObject[i].name == nameText)
            {
                findNeedText = textsOnGameObject[i];
                break;
            }
        }
        return (findNeedText);
    }

    private void AddAllTextsOnGameObject()
    {
        for (int i = 0; i < textsOnGameObject.Length; i++)
        {
            textsOnGameObject[i] = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }
}