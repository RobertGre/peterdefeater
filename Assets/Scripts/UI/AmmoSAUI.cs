using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoSAUI : MonoBehaviour
{
    public AmmoManager ammoManager;
    public TextMeshProUGUI ammoText;

    void Start()
    {
       
    }

    void Update()
    {
        /* Updates the UI text with current ammo count for the MAC and the max ammo 
         if both of the ammo manager for the weapon and the ammo texts are assigned*/
        if (ammoManager != null && ammoText != null)
        {
            ammoText.text = "MAC-10 ammo: " + ammoManager.currentAmmo + " / " + ammoManager.maxAmmo;
        }
    }
}
