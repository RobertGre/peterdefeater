using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    private float projectileLifetime = 3.5f;
    private Vector3 direction;

    // Method that sets the direction of the bullet
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    private void Start()
    {
        // Destroys the bullet after the specified lifetime
        Destroy(gameObject, projectileLifetime);
    }

    private void Update()
    {
        // Moves the bullet in the set direction
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the collision is with the player
        if (other.CompareTag("Player"))
        {
            // Damages the player and destroys the bullet
            other.GetComponent<Player>().TakeDamage(5);
            Destroy(gameObject);
        }
    }
}