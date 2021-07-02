using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IItemUsed
{
    [HideInInspector] public InventorySlot inventorySlotItem { get; set; }
    [HideInInspector] public GameObject player { get; set; }
    
    private Item _foodItem;

    private void Start()
    {
        _foodItem = gameObject.GetComponent<Item>();
    }

    public void UseItem()
    {
        if (inventorySlotItem.Amount <= 0)
            Destroy(gameObject);
        else
            EatFood();
    }

    private void EatFood()
    {
        SatietyPlayer satietyPlayer = player.GetComponent<SatietyPlayer>();
        FoodObject foodObject = (FoodObject)_foodItem.item;
        if (satietyPlayer.CurrentSatiety < 100)
        {
            satietyPlayer.UpSatiety(foodObject.saturation);
            inventorySlotItem.SubstractAmount(_foodItem.countCopy);
        }
        //Debug.Log(foodObject.saturation);
        //satietyPlayer.UpSatiety(_foodItem.item.);
    }
}
