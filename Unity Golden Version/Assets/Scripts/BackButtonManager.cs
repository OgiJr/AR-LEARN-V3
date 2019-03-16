using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public GameObject backButton;

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

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
