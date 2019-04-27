using System.Collections;
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
    }

    private void Update()
    {
        if (searchedWord == "Заптие")
        {
            output.text = "Турски стражар у нас преди Освобождението.";
        }
    }
}
