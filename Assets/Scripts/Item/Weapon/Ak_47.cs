using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak_47 : MonoBehaviour, IItemUsed, IWeapon
{
    public InventorySlot inventorySlotItem { get; set; }
    public GameObject player { get; set; }
    //public GameObject prefabBullet;
    public GameObject spawnBullet;

    private WeaponStorage weaponStorage;
    private Item _weaponItem;
    private WeaponObject _weaponObject;

    private float timeShoot = 0.0f;
    private float currentReload = 0.0f;
    private float bulletInMinute;
    //private int currentBullet;
    private bool activateReloading;
    private bool activateScript = false;
    private void Start()
    {
        _weaponItem = gameObject.GetComponent<Item>();
        _weaponObject = (WeaponObject)_weaponItem.item;
        weaponStorage = gameObject.GetComponent<WeaponStorage>();

        bulletInMinute = 60.0f / _weaponObject.rateFire;
        activateReloading = false;
        player = inventorySlotItem.player;
    }

    private void Update()
    {
        if (activateScript == true)
        {
            if (Input.GetKey(KeyCode.R))
                activateReloading = true;

            if (currentReload > _weaponObject.timeReload)
                Reload();

            timeShoot += Time.deltaTime;
            if (activateReloading)
                currentReload += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            if (weaponStorage.CurrentAmmo <= 0)
                weaponStorage.CurrentAmmo = 0;
            if ((timeShoot > bulletInMinute) && weaponStorage.CurrentAmmo > 0)
                Shoot();
        }
    }

    public void UseItem() 
    {
        activateScript = true;
        //Shoot();
    }

    public void Shoot()
    {
        weaponStorage.CurrentAmmo--;
        GameObject objectBullet = Instantiate(_weaponObject.prefabBullet, spawnBullet.transform.position, spawnBullet.transform.rotation);
        Rigidbody rigidbody = objectBullet.GetComponent<Rigidbody>();
        rigidbody.AddForce(gameObject.transform.forward * _weaponObject.speedBullet, ForceMode.VelocityChange);
        Destroy(objectBullet, 10f);
        timeShoot = 0.0f;
    }

    public void Reload()
    {
        if (weaponStorage.currentMaxAmmo > 0)
        {
            for (int i = weaponStorage.currentAmmo; i < _weaponObject.сartridgeInTheHorn; i++)
            {
                if (weaponStorage.currentMaxAmmo == 0)
                    break;
                weaponStorage.currentAmmo++;
                weaponStorage.currentMaxAmmo--;
            }
            currentReload = 0.0f;
            activateReloading = false;
        }
    }

    private void OnDestroy()
    {
        if (this.weaponStorage != null && inventorySlotItem != null)
        {
            //Debug.Log(this.weaponStorage.currentAmmo);
            inventorySlotItem.StorageItem = (IStorageItem)this.weaponStorage;
            WeaponStorage weaponStorage1 = (WeaponStorage)inventorySlotItem.StorageItem;
        }
    }
}
