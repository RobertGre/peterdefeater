using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public GameObject keyPrefab; // Key object to drop

    // Method to instantiate key object
    public void DropKey()
    {
        Instantiate(keyPrefab, transform.position, Quaternion.identity);
    }
}
