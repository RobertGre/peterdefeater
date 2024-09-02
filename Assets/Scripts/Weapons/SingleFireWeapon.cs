using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleFireWeapon : WeaponBase
{
    private bool isFiring = false;
    private AmmoManager ammoManager;

    void Start()
    {
        ammoManager = GetComponent<AmmoManager>();
    }

    void Update()
    {
        // Checks if the fire button is pressed, if the weapon is not already firing and if there is ammo available
        if (Input.GetButtonDown("Fire1") && !isFiring && ammoManager.currentAmmo > 0)
        {
            // Starts the fire routine
            StartCoroutine(FireRoutine());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            // Stops firing when the fire button is released
            isFiring = false;
        }
    }

    // Coroutine for firing the weapon
    IEnumerator FireRoutine()
    {
        // Sets the flag to indicate that the weapon is firing
        isFiring = true;
        FireWeapon(); // Calls the method that handles the firing logic
        ammoManager.UseAmmo(1); // Reduces the ammo count
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    // Method to fire the weapon
    public override void FireWeapon()
    {
        Vector2 spawnPosition = (Vector2)transform.position + (Vector2)(transform.up * spawnDistance);
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Sets the velocity of the projectile to make it move in the direction of the weapons forward vector
            rb.velocity = transform.up * bulletSpeed;
        }

        // Destroy the projectile after the specified lifetime
        Destroy(newProjectile, projectileLifetime);
    }
}
