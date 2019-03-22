using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SvaeInfoSceneOne : MonoBehaviour
{
    public static string info;

    public InputField saveText;
    public TMPro.TMP_InputField recieveText;

    private void Start()
    {
        if (recieveText != null)
        {
            recieveText.text = info;
        }
    }

    private void Update()
    {

        Debug.Log(info);

        if (saveText != null)
        {
            info = saveText.text;
        }
    }
}
