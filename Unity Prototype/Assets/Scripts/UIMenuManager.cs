using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Basically a ghetto PageViewer. IIt handles the menus.
/// </summary>
public class UIMenuManager : MonoBehaviour
{
    private int state = 0;
    public GameObject[] main = new GameObject[4];
    public InputField search;
    public GameObject loadingScreen;
    public GameObject scanButton;
    private bool focused = false;

    private void Update()
    {
        if (search.isFocused && !focused)
        {
            main[3].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f);
            focused = true;
            scanButton.GetComponent<Animator>().enabled = false;
            scanButton.GetComponent<Image>().color = new Color(0,0,0,0);
            Debug.Log("Howard");
        }
        else if(!search.isFocused && focused && loadingScreen.activeSelf == false)
        {
            main[3].GetComponent<RectTransform>().DOAnchorPos(new Vector3(0,1500,0), 0.5f);
            focused = false;
            scanButton.GetComponent<Animator>().enabled = true;
            scanButton.GetComponent<Image>().color = new Color(0, 0, 0, 255);
            Debug.Log("Mitchell");
        }
    }

    /// <summary>
    /// Move to the left.
    /// </summary>
    public void MinusOne()
    {
        if(state == 0)
        {
            main[0].GetComponent<RectTransform>().DOAnchorPos(new Vector3(1200, 0, 0), .5f);
            main[1].GetComponent<RectTransform>().DOAnchorPos(new Vector3(450, -700, 0), .5f);
        }
        if (state == 1)
        {
            main[0].GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 0, 0), .5f);
            main[2].GetComponent<RectTransform>().DOAnchorPos(new Vector3(2000, -700, 0), .5f);
        }
        state--;
    }

    /// <summary>
    /// Move to the right.
    /// </summary>
    public void PlusOne()
    {
        if(state == -1)
        {
            main[0].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, .5f);
            main[1].GetComponent<RectTransform>().DOAnchorPos(new Vector3(-450, -700, 0), .5f);
        }

        if (state == 0)
        {
            main[0].GetComponent<RectTransform>().DOAnchorPos(new Vector3(-1200, 0, 0), .5f);
            main[2].GetComponent<RectTransform>().DOAnchorPos(new Vector3(450, -700, 0), .5f);
        }

        state++;
    }
}
