using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    public int ammoAmount = 50;

    /*This was an attempt to create an ammo object that on pickup would give ammo to the active weapon
      by ckecking for the ammo manager on the active object but this only gives the specified ammo to
      the weapon that was active in the start (Glock) after which it destroys the intantiated ammo 
      pickup game object*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponManager weaponManager = other.GetComponentInChildren<WeaponManager>(); 

            if (weaponManager != null)
            {
                GameObject activeWeaponObject = weaponManager.GetCurrentWeapon();

                if (activeWeaponObject != null)
                {
                    AmmoManager ammoManager = activeWeaponObject.GetComponent<AmmoManager>();

                    if (ammoManager != null)
                    {
                        ammoManager.currentAmmo += ammoAmount;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
