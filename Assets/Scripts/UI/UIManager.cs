using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas deathCanvas;

    private void Start()
    {
        Time.timeScale = 0.0f;
    }

    // This method reloads the scene
    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This method quits the application when the quit button gets clicked
    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Application quit");
    }

    // This method turns the time scale on 1, disables the menu canvas and enables the hud
    public void OnPlayButtonClicked()
    {
        Time.timeScale = 1f;
        menuCanvas.gameObject.SetActive(false);
        hudCanvas.gameObject.SetActive(true);
    }

    // This method reloads the game with the menu canvas on 
    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 0f;
        menuCanvas.gameObject.SetActive(true);
        hudCanvas.gameObject.SetActive(false);
        deathCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
