﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    internal bool[] controls = { false, false, false }; //Tap, Left and Right
    internal Vector2 startPos, swipeDistance;
    private bool swiping = false;

    int TapCount;
    float MaxDubbleTapTime = 1f;
    float NewTime;

<<<<<<< HEAD
    public bool changeAnimationMode = false;
=======
    public bool changeAnimationMode = true;
>>>>>>> f658a2ea6b44743dbdcb32d1334c0903b6b8351f
    private GameObject objectHandler;

    private void Update()
    {
        SwipeDetect();

        if (objectHandler == null)
        {
            if (GameObject.Find("CloudRecognition") != null)
            {
                objectHandler = GameObject.Find("CloudRecognition").GetComponent<CloudHandler>().selectedObject;
            }
            else
            {
                objectHandler = GameObject.Find("UserDefinedTargetBuilder").GetComponent<UserDefinedMode>().augmentationObject;
            }
        }

        if (changeAnimationMode == false)
        {
            this.gameObject.GetComponent<AdjustControls>().enabled = true;
            this.gameObject.GetComponent<AnimatorManager>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<AdjustControls>().enabled = false;
            this.gameObject.GetComponent<AnimatorManager>().enabled = true;
        }
    }

    internal void SwipeDetect()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {
                NewTime = Time.time + MaxDubbleTapTime;
            }

            else if (TapCount == 2 && Time.time <= NewTime)
            {
                if (changeAnimationMode == true)
                {
                    changeAnimationMode = false;
                    Debug.Log("true");
                }
                else
                {
                    Debug.Log("false");
                    changeAnimationMode = true;
                }
            }

        }
        if (Time.time > NewTime)
        {
            TapCount = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            controls[i] = false;
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                controls[0] = true;
                startPos = Input.touches[0].position;
                swiping = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                swiping = false;
                Reset();
            }
        }

        swipeDistance = Vector2.zero;
        if (swiping == true)
        {
            if (Input.touches.Length > 0)
            {
                swipeDistance = Input.touches[0].position;

            }
        }

        if (changeAnimationMode == true)
        {
            if (swipeDistance.magnitude > 80)
            {
                if (Mathf.Abs(swipeDistance.x) >= Mathf.Abs(swipeDistance.y))
                {
                    if (swipeDistance.x > 0)
                    {
                        Debug.Log("left");
                        controls[2] = true;
                    }

                    else
                    {
                        Debug.Log("right");
                        controls[1] = true;
                    }
                }
            }
            Reset();
        }
        else
        {
            if (swipeDistance.magnitude > 80)
            {
                objectHandler.GetComponent<Animator>().enabled = false;
            }
        }
    }

    internal void Reset()
    {
        swiping = false;
        startPos = Vector2.zero;
        swipeDistance = Vector2.zero;
    }
}