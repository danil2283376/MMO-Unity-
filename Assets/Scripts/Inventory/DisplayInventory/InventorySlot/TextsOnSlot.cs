using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextsOnSlot : MonoBehaviour
{
    public GameObject gameObjectSlot;
    private TextMeshProUGUI[] textsOnGameObject;

    public TextsOnSlot(GameObject inventorySlot)
    {
        gameObjectSlot = inventorySlot;
        textsOnGameObject = new TextMeshProUGUI[inventorySlot.transform.childCount];
        AddAllTextsOnGameObject();
    }

    public TextMeshProUGUI SearchNeedText(string nameText)
    {
        TextMeshProUGUI findNeedText = null;
        for (int i = 0; i < textsOnGameObject.Length; i++)
        {
            Debug.Log(textsOnGameObject[i].name);
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
            textsOnGameObject[i] = gameObjectSlot.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }
}
