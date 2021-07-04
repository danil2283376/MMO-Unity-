using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIWeapon : MonoBehaviour
{
    public GameObject rightHandPlayer;
    public TextMeshProUGUI infoAmmo;
    public Image weaponImage;

    private GameObject _panelInfoWeapon;

    private void Start()
    {
        _panelInfoWeapon = gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (rightHandPlayer.transform.childCount > 0)
            UpdateInfoOnPanel();
        else
            _panelInfoWeapon.SetActive(false);
    }

    private void UpdateInfoOnPanel() 
    {
        GameObject gameObjectItem = rightHandPlayer.transform.GetChild(0).gameObject;
        IWeapon weapon = gameObjectItem.GetComponent<IWeapon>();
        if (weapon != null && gameObjectItem.tag == "RangeWeapon")
        {
            WeaponStorage weaponStorage = gameObjectItem.GetComponent<WeaponStorage>();
            _panelInfoWeapon.SetActive(true);
            UpdateTextOnPanel(weaponStorage);
            UpdateImageOnPanel(gameObjectItem);
        }
    }

    private void UpdateTextOnPanel(WeaponStorage weaponStorage) 
    {
        infoAmmo.text = weaponStorage.CurrentAmmo + "/" + weaponStorage.CurrentAmmo;
    }

    private void UpdateImageOnPanel(GameObject weapon)
    {
        Item weaponItem = weapon.GetComponent<Item>();
        weaponImage.sprite = weaponItem.item.sprite;
    }
}
