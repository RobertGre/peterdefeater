using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject keyObject; // Reference to the key object on the player

    /* If player enters the collider, it triggers this loop which checks for the "Player" script on the player 
     and if the player script is assigned it sets the "HasKey" bool to true and it activates the key object on the player*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.HasKey = true;
                if (keyObject != null)
                {
                    keyObject.SetActive(true);
                }
                Destroy(gameObject);
            }
        }
    }
}
