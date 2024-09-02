using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject parentObject;
    private bool allChildrenDestroyed = false;
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas winCanvas;
    // Update is called once per frame
    void Update()
    {
        // Checks if all children are destroyed and sets the win canvas to active
        if (!allChildrenDestroyed && AreChildrenDestroyed())
        {
            Time.timeScale = 0f;
            Debug.Log("Game won!");
            hudCanvas.gameObject.SetActive(false);
            winCanvas.gameObject.SetActive(true);
            allChildrenDestroyed = true;
        }
    }

    // Method to check if all children of the parent object are destroyed
    private bool AreChildrenDestroyed()
    {
        if (parentObject.transform.childCount == 0)
        {
            return true;
        }
        else
        {
            // Checks if there are any child objects active
            foreach (Transform child in parentObject.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    return false; // If at least one is active it returns false
                }
            }
            return true; // if all children are destroyed it returns true
        }
    }
}
