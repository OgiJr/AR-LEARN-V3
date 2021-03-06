﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryManager : MonoBehaviour
{
    private string searchedWord;
    public Text input;
    public Text output;

    public void Search()
    {
        searchedWord = input.text;
        string url = "https://arlearn.xyz/dictionary.php?word=" + searchedWord;
        WWW www = new WWW(url);
        while (!www.isDone) ;
        string results = www.text;
        if(results == "")
        {
            if (PlayerPrefs.GetInt("Language") == 0)
            {
                output.text = "The word is not in our database.";
            }
            else
            {
                output.text = "Няма такава дума в базата ни данни.";
            }
        } else
        {
            output.text = results;
        }
    }
}
