using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ActivateScript : MonoBehaviour
{
    public MonoBehaviour[] scriptsForActivate;

    public void ActivateScriptsOnObject() 
    {
        for (int component = 0; component < scriptsForActivate.Length; component++)
            scriptsForActivate[component].enabled = true;
    }
}
