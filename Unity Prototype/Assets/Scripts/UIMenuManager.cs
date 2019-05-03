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
    public GameObject down;

    private void Update()
    {
        if (search.isFocused && !focused)
        {
            main[3].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f);
            focused = true;
            scanButton.GetComponent<Animator>().enabled = false;
            scanButton.GetComponent<Image>().color = new Color(0,0,0,0);
        }
        else if(!search.isFocused && focused)
        {
            focused = false;
            Invoke("Remove", 0.5f);
        }
    }

    void Remove()
    {
        main[3].GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 1500, 0), 1);
        scanButton.GetComponent<Animator>().enabled = true;
        scanButton.GetComponent<Image>().color = new Color(0, 0, 0, 255);
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

    public void Up()
    {
        main[0].GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, .5f);
        down.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-320, -2550), 0.5f);
    }

    public void Down()
    {
        main[0].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 3000), .5f);
        down.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-320 , -870), 0.5f);
    }
}
