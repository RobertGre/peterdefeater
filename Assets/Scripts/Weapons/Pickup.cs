using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject singleFireWeapon;
    public GameObject semiAutoWeapon;
    public GameObject automaticWeapon;

    public TextMeshProUGUI sText;
    public TextMeshProUGUI saText;
    public TextMeshProUGUI aText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivates weapons and hides text objects when colliding with a pickup object
            singleFireWeapon.SetActive(false);
            sText.gameObject.SetActive(false);
           
            semiAutoWeapon.SetActive(false);
            saText.gameObject.SetActive(false);
            
            automaticWeapon.SetActive(false);
            aText.gameObject.SetActive(false);

            // Check which pickup object the player collided with
            if (gameObject.name == "PickupSAweapon")
            {
                // Activates the semi-automatic weapon and displays its text
                semiAutoWeapon.SetActive(true);
                saText.gameObject.SetActive(true);
            }
            else if (gameObject.name == "PickupAweapon")
            {
                // Activates the automatic weapon and displays its text
                automaticWeapon.SetActive(true);
                aText.gameObject.SetActive(true);
            }

            // Destroys the pickup object
            Destroy(gameObject);
        }
    }
}