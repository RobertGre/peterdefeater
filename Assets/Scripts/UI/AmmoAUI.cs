using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoAUI : MonoBehaviour
{
    public AmmoManager ammoManager;
    public TextMeshProUGUI ammoText;

    void Start()
    {

    }

    void Update()
    {
        /* Updates the UI text with current ammo count for the AK and the max ammo 
         if both of the ammo manager for the weapon and the ammo texts are assigned*/
        if (ammoManager != null && ammoText != null)
        {
            ammoText.text = "AK-47 ammo: " + ammoManager.currentAmmo + " / " + ammoManager.maxAmmo;
        }
    }
}
