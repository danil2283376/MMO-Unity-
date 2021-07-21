using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Fill_Inventory : MonoBehaviour
{
    public ItemDatabaseObject database;
    [SerializeField] private InventoryObject inventory;
    public string savePath = "/FastSlots.save";
    private void Awake()
    {
        GameObject inventoryPanel = transform.gameObject;

        int countInventorySlots = inventoryPanel.transform.childCount;
        //BinaryFormatter bf = new BinaryFormatter();
        //if (File.Exists(string.Concat(Application.persistentDataPath, savePath)) == false)
        //{
        //    File.Create(string.Concat(Application.persistentDataPath, savePath));
        //}
        FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        Debug.Log(string.Concat(Application.persistentDataPath, savePath));
        for (int i = 0; i < countInventorySlots; i++)
        {
            GameObject gameObjectSlot = inventoryPanel.transform.GetChild(i).gameObject;
            InventorySlot inventorySlot = gameObjectSlot.GetComponent<InventorySlot>();
            inventorySlot.numberInventorySlot = i;
            inventorySlot.id = -1;
            //LoadInventorySlot(ref inventorySlot, i);
            inventory.AddInventorySlot(gameObjectSlot);
            if (file.Length != 0)
                LoadInventorySlot(ref file, ref inventorySlot);
        }
        file.Close();
    }

    private void LoadInventorySlot(ref FileStream file, ref InventorySlot inventorySlot)
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)) == true)
        {
            Debug.Log("file.Length: " + file.Length);
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
            //Debug.Log(string.Concat(Application.persistentDataPath, savePath));

            BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);

            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), inventorySlot);
            if (inventorySlot.id != -1)
            {
                if (database == null)
                    Debug.Log("Database null");
                inventorySlot.ItemObjectInSlot = database.GetItem[inventorySlot.id];
                inventorySlot.LoadItemSlot(file, bf);
            }
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), inventory[i]._itemObjectInSlot);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), inventory[i]._storageItem);
            //Debug.Log("Load!");
            //Debug.Log(inventory[0].ItemObjectInSlot.description);
            //Debug.Log(inventory[0].id);
        }
        //Debug.Log("inventory.Count: " + inventory.Count);
    }
}
