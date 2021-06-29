using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesPlayer : MonoBehaviour
{
    [HideInInspector] public GameObject headPlayer;
    [HideInInspector] public GameObject bodyPlayer;
    [HideInInspector] public GameObject leftArm;
    [HideInInspector] public GameObject rightArm;

    private GameObject _player;

    private void Awake()
    {
        this._player = gameObject;
        GetBonePlayer();
    }

    private void GetBonePlayer()
    {
        FindAndGetBone(ref headPlayer, "Head");
        FindAndGetBone(ref bodyPlayer, "Body");
        FindAndGetArm(ref rightArm, ref bodyPlayer, "Right arm");
        FindAndGetArm(ref leftArm, ref bodyPlayer, "Left arm");
    }

    private void FindAndGetBone(ref GameObject bonePlayer, string nameBone)
    {
        for (int i = 0; i < _player.transform.childCount; i++)
        {
            GameObject childPlayer = _player.transform.GetChild(i).gameObject;
            if (childPlayer.name == nameBone)
            {
                bonePlayer = childPlayer;
                break;
            }
        }
    }

    private void FindAndGetArm(ref GameObject arm, ref GameObject bodyPlayer, string nameArm)
    {
        for (int i = 0; i < bodyPlayer.transform.childCount; i++)
        {
            GameObject childBody = bodyPlayer.transform.GetChild(i).gameObject;
            if (childBody.name == nameArm)
            {
                arm = childBody;
                break;
            }
        }
    }
}
