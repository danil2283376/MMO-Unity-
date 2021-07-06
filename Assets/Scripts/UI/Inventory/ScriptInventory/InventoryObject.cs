using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory Object",
    menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDatabaseObject database;
    public List<InventorySlot> inventory = new List<InventorySlot>();
    public int maxAmountInventorySlot;

    private bool inventoryIsFull { get; set; } = false;
    public bool InventoryIsFull
    {
        get 
        {
            return (inventoryIsFull);
        }
        private set 
        {
            inventoryIsFull = value;
        }
    }

    public void AddInventorySlot(GameObject gameObjectSlot)
    {
        if (database == null)
            throw new InvalidOperationException("DATABASE NULL!!!");
        InventorySlot inventorySlot1 = gameObjectSlot.GetComponent<InventorySlot>();
        inventorySlot1.maxAmount = maxAmountInventorySlot;
        inventory.Add(inventorySlot1);
    }

    public InventorySlot InventoryFindItem(ItemObject itemObject)
    {
        if (itemObject != null)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ItemObjectInSlot != null)
                {
                    if (itemObject.typeItem != TypeItem.Default)
                        if (inventory[i].ItemObjectInSlot.typeItem == itemObject.typeItem
                            && inventory[i].ItemObjectInSlot.name == itemObject.name)
                            return (inventory[i]);
                }
            }
        }
        return (null);
    }

    public void AddItemInSlot(ItemObject itemObject, int amount, IStorageItem storageItem)
    {
        if (itemObject != null)
        {
            InventorySlot findInventorySlot = InventoryFindItem(itemObject);
            // Если мы не нашли похожего предмета
            if (findInventorySlot == null ||
                findInventorySlot.SlotIsFull == true)
            {
                // Ищу пустое место под предмет
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].ItemObjectInSlot == null)
                    {
                        inventory[i].SetValueInSlot(database.GetId[itemObject], itemObject, amount, storageItem);
                        Debug.Log("Id item: " + inventory[i].id);
                        break;
                    }
                }
                CheckInventoryFull(itemObject);
            }
            else
                findInventorySlot.AddAmount(amount);
        }
    }

    public void CheckInventoryFull(ItemObject itemObject)
    {
        int i = 0;
        if (itemObject != null)
        {
            for (i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ItemObjectInSlot == null)
                    break;
                if (inventory[i].ItemObjectInSlot.typeItem == itemObject.typeItem)
                {
                    if (inventory[i].ItemObjectInSlot.name == itemObject.name)
                    {
                        if (inventory[i].SlotIsFull == true)
                            continue;
                        else
                            break;
                    }
                }
            }
        }
        if (i == inventory.Count)
            inventoryIsFull = true;
        else
            inventoryIsFull = false;
    }

    public void Save() 
    {
        //Debug.Log("\n\n\n\n");
        //string saveData = JsonUtility.ToJson(this, true);
        //string saveInventorySlots = JsonUtility.ToJson(inventory, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        for (int i = 0; i < inventory.Count; i++)
        {
            //Debug.Log(inventory[i].id);
            string saveInventorySlots = JsonUtility.ToJson(inventory[i], true);
            bf.Serialize(file, saveInventorySlots);
        }
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)) == true)
        {
            //Debug.Log(string.Concat(Application.persistentDataPath, savePath));
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            for (int i = 0; i < inventory.Count; i++)
            {
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), inventory[i]);
            }
            file.Close();
            //Debug.Log("Load!");
            //Debug.Log(inventory[0].ItemObjectInSlot.description);
            Debug.Log(inventory[0].id);
        }
        Debug.Log("inventory.Count: " + inventory.Count);
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
                throw new InvalidOperationException("NULL");
            if (inventory[i].id != -1)
            {
                Debug.Log(inventory[i].id);
                inventory[i].ItemObjectInSlot = database.GetItem[inventory[i].id];
            }
        }
    }

    public void OnAfterDeserialize()
    {
        //for (int i = 0; i < inventory.Count; i++)
        //{
        //    if (inventory[i] == null)
        //        throw new InvalidOperationException("NULL");
        //    if (inventory[i].id != -1)
        //    {
        //        Debug.Log(inventory[i].id);
        //        inventory[i].ItemObjectInSlot = database.GetItem[inventory[i].id];
        //    }
        //}
        //throw new System.NotImplementedException();
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}