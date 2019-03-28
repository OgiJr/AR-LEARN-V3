using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the back button on Android which basically does the same thing as the red cross button.
/// </summary>
public class BackButtonManager : MonoBehaviour
{
    public GameObject backButton;

    /// <summary>
    /// The button which gives you the ability to go back into online mode is activated only if you have mobile data or are connected to WiFi.
    /// </summary>
    private void Update()
    {
        if(Application.internetReachability != NetworkReachability.NotReachable && backButton.activeSelf != true)
        {
            backButton.SetActive(true);
        }
        else if(Application.internetReachability == NetworkReachability.NotReachable && backButton.activeSelf == true)
        {
            backButton.SetActive(false);
        }
    }

    /// <summary>
    /// Loads the original scene when pressed.
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
