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
        if(textUI.text.Length >= 8)
        {
            Search();
        }
    }

    //Assign to button
    public void Search()
    {
        if (serverDownloader.p != null && serverDownloader.p.bundle != null)
        {
            foreach (AssetBundle ab in serverDownloader.p.bundle)
            {
                if (ab != null)
                {
                    ab.Unload(true);
                }
            }
        }
        serverDownloader.getInfo(text);
        textUI.text = "";
        serverDownloader.downloadModels();
    }
}