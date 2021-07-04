using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak_47 : MonoBehaviour, IItemUsed, IWeapon
{
    public InventorySlot inventorySlotItem { get; set; }
    public GameObject player { get; set; }
    //public GameObject prefabBullet;
    public GameObject spawnBullet;

    private Item _weaponItem;
    private WeaponObject _weaponObject;

    private float timeShoot = 0.0f;
    private float currentReload = 0.0f;
    private float bulletInMinute;
    private int currentBullet;
    private bool activateReloading;

    private void Start()
    {
        _weaponItem = gameObject.GetComponent<Item>();
        _weaponObject = (WeaponObject)_weaponItem.item;

        bulletInMinute = 60.0f / _weaponObject.rateFire;
        currentBullet = _weaponObject.сartridgeInTheHorn;
        activateReloading = false;
        player = inventorySlotItem.player;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
            activateReloading = true;

        if (currentReload > _weaponObject.timeReload)
            Reload();

        timeShoot += Time.deltaTime;
        if (activateReloading)
            currentReload += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            if (currentBullet <= 0)
                currentBullet = 0;
            else if ((timeShoot > bulletInMinute) && currentBullet > 0)
                Shoot();
        }
    }

    public void UseItem() 
    {
        //Shoot();
    }

    public void Shoot()
    {
        currentBullet--;
        GameObject objectBullet = Instantiate(_weaponObject.prefabBullet, spawnBullet.transform.position, spawnBullet.transform.rotation);
        Rigidbody rigidbody = objectBullet.GetComponent<Rigidbody>();
        rigidbody.AddForce(gameObject.transform.forward * _weaponObject.speedBullet, ForceMode.VelocityChange);
        Destroy(objectBullet, 10f);
        timeShoot = 0.0f;
    }

    public void Reload()
    {
        currentBullet = _weaponObject.сartridgeInTheHorn;
        currentReload = 0.0f;
        activateReloading = false;
    }
}
