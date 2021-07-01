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
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //    UseItemInHand();
    }

    //private void UseItemInHand() 
    //{
    //    _bonesPlayer.bodyPlayer.
    //}
}
