using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    // Ammo data
    public int maxAmmo = 50;
    public int currentAmmo { get; set; }

    private Animator playerAnimator;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        playerAnimator = GetComponent<Animator>();
    }

    // Method to reduce ammo count
    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0)
        {
            currentAmmo = 0;
            isShooting = false;
        }

        isShooting = true;
    }

    void Update()
    {
        // Updates the shooting animation parameter in the animator if the player animator is assigned
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("Shooting", isShooting);
        }
    }
}
