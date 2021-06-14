using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object",
    menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int saturation;

    public void Awake()
    {
        typeItem = TypeItem.Food;
    }
}
