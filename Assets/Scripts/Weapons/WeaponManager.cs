using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    private int activeWeaponIndex = 0;

    /* Similar to the ammo pickup this was an attempt to create a sort of a weapon
       manager script that would assign the ammo manager component from the active
       weapon stored in the "activeWeaponIndex" to the "activeWeapon" and then add
       ammo to the ammo manager component but it only adds ammo to the weapon 
       active in the start */
    public void PickUpAmmo(int amount)
    {
        // Gets the active weapon
        GameObject activeWeaponObject = GetCurrentWeapon();
        WeaponBase activeWeapon = activeWeaponObject.GetComponent<WeaponBase>();

        // It was supposed to add ammo to the AmmoManager of the active weapon
        if (activeWeapon != null)
        {
            AmmoManager ammoManager = activeWeapon.GetComponent<AmmoManager>();
            if (ammoManager != null)
            {
                ammoManager.currentAmmo += amount;
            }
        }
    }

    // Method that gets the active weapon from the weapon index
    public GameObject GetCurrentWeapon()
    {
        return weapons[activeWeaponIndex];
    }
}