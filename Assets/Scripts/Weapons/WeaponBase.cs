using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // Common properties for all weapons
    public GameObject projectilePrefab;
    public Rigidbody2D projectileRB;
    public float bulletSpeed = 2.0f;
    public float projectileLifetime = 3.5f;
    public float fireRate = 0.5f;
    public float spawnDistance = 1.0f;

    // Abstract method for firing the weapon
    public abstract void FireWeapon();
}
