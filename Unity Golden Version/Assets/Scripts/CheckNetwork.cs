using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckNetwork : MonoBehaviour
{
    static private bool changedWiFi = false;
    static private bool changedData = false;
    static private bool changedOffline = false;

    public GameObject mobileDataUI;

    public GameObject scanButton;
    public GameObject exitButton;

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

    public void OnlineMode()
    {
        SceneManager.LoadScene(0);
    }

    public void OfflineMode()
    {
        SceneManager.LoadScene(1);
    }
}