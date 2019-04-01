using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Checks the network so the app can switch from and to online mode.
/// </summary>
public class CheckNetwork : MonoBehaviour
{
    static private bool changedWiFi = false;
    static private bool changedData = false;
    static private bool changedOffline = false;

    public GameObject mobileDataUI;

    public GameObject scanButton;
    public GameObject exitButton;

    /// <summary>
    /// 1) Manages the user interface so that you can choose whether you want to be in offline mode or in online mode when mobile data is detected.
    /// 2) Goes in online mode when is connected to WiFi.
    /// 3) Goes in offline mode when there is no mobile data nor any WiFi.
    /// <para name = "Data">The current mode, whether it is offline, online, etc.</para> 
    /// <return>The current mode.</return>
    /// </summary>
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork && changedWiFi == false)
        {
            changedOffline = false;
            changedWiFi = true;
            changedData = false;

            SceneManager.LoadScene(0);
        }

        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork && changedData == false)
        {
            mobileDataUI.SetActive(true);

            changedOffline = false;
            changedWiFi = false;
            changedData = true;

            if (scanButton.activeSelf == true)
            {
                scanButton.SetActive(false);
            }

            if (exitButton.activeSelf == true)
            {
                exitButton.SetActive(false);
            }
        }
        else if (Application.internetReachability == NetworkReachability.NotReachable && changedOffline == false)
        {
            changedOffline = true;
            changedWiFi = false;
            changedData = false;

            SceneManager.LoadScene(1);
        }
    }

    /// <summary>
    /// If this button is pressed Unity loads the online mode scene.
    /// </summary>
    public void OnlineMode()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// If this button is pressed Unity loads the offline mode scene.
    /// </summary>
    public void OfflineMode()
    {
        SceneManager.LoadScene(1);
    }
}