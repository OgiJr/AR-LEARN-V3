﻿using System.Collections;
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
    public GameObject[] main = new GameObject[3];

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
