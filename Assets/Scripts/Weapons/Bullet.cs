using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the collision is with an enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Check if the collision is with a collider that is not a trigger (obstacles) 
         * and if the collision is with one then it destroys the bullet */
        if (!collision.collider.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
