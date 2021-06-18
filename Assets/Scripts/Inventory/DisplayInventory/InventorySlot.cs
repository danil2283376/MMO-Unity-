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
    [HideInInspector]
    public int numberInventorySlot;
    [HideInInspector]
    public GameObject gameObjectSlot;

    private Image _imageOnSlot;
    private TextMeshProUGUI _countItems;
    private TextsOnSlot textsOnSlot;
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
        slotIsFull = copy.slotIsFull;
        _imageOnSlot = gameObjectSlot.GetComponent<Image>();
        // Add all TextMeshProUGUI on slot
        textsOnSlot = new TextsOnSlot(gameObjectSlot);
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
        //_imageOnSlot = gameObjectSlot.GetComponent<Image>();
        _imageOnSlot.sprite = itemObject.sprite;
        UpdateTextInventorySlot();
    }

    private void UpdateTextInventorySlot()
    {
        if (slotIsFull == false)
        {
            if (itemObject != null)
            {
                //TextMeshProUGUI textMeshProUGUI = gameObjectSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI countItems = textsOnSlot.SearchNeedText("NumberSlot");
                //Debug.Log(textMeshProUGUI.name);
                if (countItems != null)
                    countItems.text = amount + "/" + maxAmount;
            }
        }
    }

    //private TextMeshProUGUI FindOnGameObjectText(string nameFindGameObject)
    //{
    //    TextMeshProUGUI findText = null;

    //    for (int i = 0; i < gameObjectSlot.transform.childCount; i++)
    //    {
    //        GameObject gameObject = gameObjectSlot.transform.GetChild(i).gameObject;
    //        if (gameObject.name == nameFindGameObject)
    //        {
    //            findText = gameObject.GetComponent<TextMeshProUGUI>();
    //            break ;
    //        }
    //    }
    //    return (findText);
    //}
}

class TextsOnSlot
{
    public string nameText;
    public GameObject gameObject;
    private TextMeshProUGUI[] textsOnGameObject;

    public TextsOnSlot(GameObject gameObjectWithTexts)
    {
        gameObject = gameObjectWithTexts;
        textsOnGameObject = new TextMeshProUGUI[gameObjectWithTexts.transform.childCount];
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

    //public TextMeshProUGUI FindOnGameObjectText(string nameFindGameObject)
    //{
    //    TextMeshProUGUI findText = null;

    //    for (int i = 0; i < gameObjectText.transform.childCount; i++)
    //    {
    //        GameObject gameObject = gameObjectText.transform.GetChild(i).gameObject;
    //        if (gameObject.name == nameFindGameObject)
    //        {
    //            findText = gameObject.GetComponent<TextMeshProUGUI>();
    //            break;
    //        }
    //    }
    //    return (findText);
    //}
}