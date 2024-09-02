using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AmmoUI : MonoBehaviour
{
    private AmmoManager ammoManager;
    private TextMeshProUGUI ammoText;
    /* This script finds the "AmmoManager" and the UI text components from
       the default weapon, this works without the need to assign the components
       manually because the default weapon and the text are active at the start
       of the scene */
    void Start()
    {
        if (ammoManager == null)
        {
            ammoManager = FindObjectOfType<AmmoManager>();
        }

        if (ammoText == null)
        {
            ammoText = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        // Update the UI text with current ammo count for the glock and the max ammo
        if (ammoManager != null && ammoText != null)
        {
            ammoText.text = "Glock ammo: " + ammoManager.currentAmmo + " / " + "∞";
        }
    }
}
