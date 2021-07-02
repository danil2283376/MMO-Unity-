using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BonesPlayer))]
public class UseItem : MonoBehaviour
{
    private BonesPlayer _bonesPlayer;

    private void Start()
    {
        _bonesPlayer = gameObject.GetComponent<BonesPlayer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            UseItemInHand();
    }

    private void UseItemInHand()
    {
        GameObject rightHand = _bonesPlayer.rightArm.transform.GetChild(0).gameObject;
        if (rightHand.transform.childCount == 1)
        {
            IItemUsed itemUsed = rightHand.transform.GetChild(0).GetComponent<IItemUsed>();
            itemUsed.UseItem();
        }
        else if (rightHand.transform.childCount > 1)
        {
            throw new InvalidOperationException("Item not be must > 1");
        }
    }
}
