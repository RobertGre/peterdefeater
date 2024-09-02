using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutomaticWeapon : WeaponBase
{
    private AmmoManager ammoManager;
    public GameObject singleFireWeapon;
    public TextMeshProUGUI sText;
    public TextMeshProUGUI aText;

    private Coroutine firingCoroutine;

    void Start()
    {
        // Gets the reference from the AK "AmmoManager" component
        ammoManager = GetComponent<AmmoManager>();
    }

    void Update()
    {
        // Checks if the fire button is pressed and if there is ammo available
        if (Input.GetButtonDown("Fire1") && ammoManager.currentAmmo > 0)
        {
            // Starts firing
            StartFiring();
        }
        // Checks if the fire button is released
        else if (Input.GetButtonUp("Fire1"))
        {
            // Stops firing
            StopFiring();
        }
    }

    // Method that starts firing
    void StartFiring()
    {
        // Checks if the firing coroutine is not already running
        if (firingCoroutine == null)
        {
            // Starts the firing coroutine
            firingCoroutine = StartCoroutine(FireRoutine());
        }
    }

    // Method that stops firing
    void StopFiring()
    {
        // Checks if the firing coroutine is running
        if (firingCoroutine != null)
        {
            // Stops the firing coroutine
            StopCoroutine(firingCoroutine);
            // Resets the firing coroutine reference
            firingCoroutine = null;
        }
    }

    // Coroutine that handles continuous firing
    IEnumerator FireRoutine()
    {
        while (ammoManager.currentAmmo > 0)
        {
            FireWeapon();
            ammoManager.UseAmmo(1);
            yield return new WaitForSeconds(fireRate);
        }

        // Switches back to single fire weapon if ammo runs out
        SwitchToSingleFireWeapon();
    }

    // Method to fire the weapon
    public override void FireWeapon()
    {
        Vector2 spawnPosition = (Vector2)transform.position + (Vector2)(transform.up * spawnDistance);
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        // Checks if the Rigidbody2D component exists
        if (rb != null)
        {
            // Sets the velocity of the projectile
            rb.velocity = transform.up * bulletSpeed;
        }

        // Destroys the projectile after its lifetime expires
        Destroy(newProjectile, projectileLifetime);
    }

    // Method that switches back to the single fire weapon
    private void SwitchToSingleFireWeapon()
    {
        gameObject.SetActive(false);
        singleFireWeapon.SetActive(true);
        sText.gameObject.SetActive(true);
        aText.gameObject.SetActive(false);
    }
}
