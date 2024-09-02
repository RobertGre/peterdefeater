using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Method that handles door interaction
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            // Checks if the player has the key
            if (player != null && player.HasKey)
            {
                // Opens the door (by deactivating it)
                gameObject.SetActive(false);

                // Resets key-related attributes on the player
                player.HasKey = false;
                player.KeyObject.SetActive(false);
            }
        }
    }
}
