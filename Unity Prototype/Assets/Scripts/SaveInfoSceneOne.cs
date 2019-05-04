using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Save the info fom th TMPro input field.
/// </summary>
public class SaveInfoSceneOne : MonoBehaviour
{
    public static string info;

    public InputField saveText;
    public TMPro.TMP_InputField recieveText;

    private void Start()
    {
        if (recieveText != null)
        {
            recieveText.text = PlayerPrefs.GetString("UDTID");
        }
    }

    private void Update()
    {
        if (saveText != null)
        {
            PlayerPrefs.SetString("UDTID", recieveText.text);
            info = PlayerPrefs.GetString("UDTID");
        }
        PlayerPrefs.SetString("UDTID", recieveText.text);
        info = PlayerPrefs.GetString("UDTID");
    }
}
