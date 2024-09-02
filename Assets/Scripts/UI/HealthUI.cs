using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    private TMP_Text healthText;
    private Player player;

    void Start()
    {
        // Finds the Player object and gets its "Player" component
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Gets the "TMP_Text" component of the TextMeshPro Text element
        healthText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        // Updates the text to display the player's health
        healthText.text = "Health: " + player.Health.ToString();
    }
}
