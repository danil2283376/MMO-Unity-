using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesOnSlot : MonoBehaviour
{
    public GameObject gameObjectSlot;
    public Image imageInSlot;
    public Image borderInSlot;
    public Color idleImage;
    public Color activeImage;
    public bool activeSlot;
    public ImagesOnSlot(GameObject inventorySlot, Color _idleImage, Color _activeImage)
    {
        gameObjectSlot = inventorySlot;
        idleImage = _idleImage;
        activeImage = _activeImage;
        SearchNeedImage("ImageSlot", ref imageInSlot);
        SearchNeedImage("BorderSlot", ref borderInSlot);
    }

    public void ActivateImage()
    {
        if (activeSlot == true)
            borderInSlot.color = activeImage;
        else
            borderInSlot.color = idleImage;
    }

    private void SearchNeedImage(string nameImage, ref Image imageForWriteFindImage)
    {
        for (int i = 0; i < gameObjectSlot.transform.childCount; i++)
        {
            Transform child = gameObjectSlot.transform.GetChild(i);
            if (child.name == nameImage)
            {
                imageForWriteFindImage = child.gameObject.GetComponent<Image>();
                break;
            }
        }
    }
}
