﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectOfflineManager : MonoBehaviour
{
    public Text text;
    public ServerDownloader svDownloader;

    public void SelectObject()
    {
        PlayerPrefs.SetString("UDTID", text.text);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        foreach (AssetBundle ab in svDownloader.p.bundle)
        {
            ab.Unload(true);
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
