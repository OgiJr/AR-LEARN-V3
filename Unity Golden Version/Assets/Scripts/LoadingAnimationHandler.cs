using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimationHandler : MonoBehaviour
{
    public GameObject loadingScreen;

    private void Awake()
    {
        loadingScreen.SetActive(false);
    }

    public void LoadAnimation()
    {
        loadingScreen.SetActive(true);
        Invoke("TurnOff", 4);
    }

    void TurnOff()
    {
        loadingScreen.SetActive(false); 
    }
}
