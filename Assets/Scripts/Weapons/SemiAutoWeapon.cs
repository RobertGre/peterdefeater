using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SemiAutoWeapon : WeaponBase
{
    private bool isFiring = false;
    private AmmoManager ammoManager;
    public int bulletsPerShot = 3;
    public GameObject singleFireWeapon;

    public TextMeshProUGUI sText;
    public TextMeshProUGUI saText;

    void Start()
    {
        // Gets the reference to the AmmoManager component
        ammoManager = GetComponent<AmmoManager>();
    }

    void Update()
    {
        // Checks if the fire button is pressed,if the player is not already firing, and if there is ammo available
        if (Input.GetButtonDown("Fire1") && !isFiring && ammoManager.currentAmmo > 0)
        {
            // Start firing
            StartCoroutine(FireRoutine());
        }
        // Checks if the fire button is released
        else if (Input.GetButtonUp("Fire1"))
        {
            // Stops firing
            isFiring = false;
        }
    }

    // Coroutine to handle semi-automatic firing
    IEnumerator FireRoutine()
    {
        // Set isFiring flag to true
        isFiring = true;

        // Fires the specified number of bullets per shot
        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Fires the weapon
            FireWeapon();
            // Decreases ammo count
            ammoManager.UseAmmo(1);
            // Waits for the specified fire rate(delay) before firing the next shot
            yield return new WaitForSeconds(fireRate);
        }

        // Sets isFiring flag to false after firing is complete
        isFiring = false;

        // Checks if ammo has run out of ammo, if so, switches back to single fire weapon
        if (ammoManager.currentAmmo <= 0)
        {
            SwitchToSingleFireWeapon();
        }
    }

    // Method to fire the weapon
    public override void FireWeapon()
    {
        Vector2 spawnPosition = (Vector2)transform.position + (Vector2)(transform.up * spawnDistance);
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
        Destroy(newProjectile, projectileLifetime);
    }

    // Method to switch back to single fire weapon and the SFW ammo text
    private void SwitchToSingleFireWeapon()
    {
        gameObject.SetActive(false);
        singleFireWeapon.SetActive(true);
        sText.gameObject.SetActive(true);
        saText.gameObject.SetActive(false);
    }
}
