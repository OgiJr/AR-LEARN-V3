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

    private void Update()
    {
        text = textUI.text;
        Debug.Log(text);
    }

    //Assign to button
    public void Search()
    {
        serverDownloader.getInfo(text);
        textUI.text = "";
        serverDownloader.downloadModels();
    }
}