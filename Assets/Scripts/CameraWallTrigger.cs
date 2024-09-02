using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraWallTrigger : MonoBehaviour
{
    public Transform cameraSpot;
    public GameObject wallObject;

    private bool triggered = false; // Flag to prevent repeated triggering

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            // Moves the camera to the designated position by assigning an empty game object
            Camera.main.transform.position = cameraSpot.position;

            // Activates the wall object
            if (wallObject != null)
            {
                wallObject.SetActive(true);
            }

            // Sets triggered flag to prevent repeated triggering
            triggered = true;
        }
    }
}