using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 25; // Amount of health to add

    /* If player enters the collider, it triggers this loop which checks for the "Player" script on the player 
     and if the player script is assigned it gives the player the specified amount of health and destroys the
     instantiated game object*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Heal(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}