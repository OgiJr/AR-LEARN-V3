using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectOfflineManager : MonoBehaviour
{
    public void SelectObject()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void TextRecognition()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            SceneManager.LoadScene(2);
        }
    }
}
