using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchButtonHandler : MonoBehaviour
{
    private string text;
    public InputField textUI;
    public ServerDownloader serverDownloader;

    public GameObject loadingScreen;

    private void Update()
    {
        text = textUI.text;
        Debug.Log(text);
    }

    //Assign to button
    public void Search()
    {
        loadingScreen.SetActive(true);
        Invoke("StartSearch", 1);
    }

    private void StartSearch()
    {
        if (serverDownloader.p != null && serverDownloader.p.bundle != null)
        {
            foreach(AssetBundle ab in serverDownloader.p.bundle)
            {
                ab.Unload(true);
            }
        }
        serverDownloader.getInfo(text);
        textUI.text = "";
        serverDownloader.downloadModels();
        loadingScreen.SetActive(false);
    }
}