using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages swipe controls for transformations of the AR Object.
/// </summary>
public class SwipeControls : MonoBehaviour
{
    internal bool[] controls = { false, false, false }; //Tap, Left and Right
    internal Vector2 startPos, swipeDistance;
    private bool swiping = false;

    int TapCount;
    float maxDoubleTapTime = .5f;
    float newTime;

    public bool changeAnimationMode = false;
    private GameObject objectHandler;

    Vector3 OGSize;

    /// <summary>
    /// Finds the original size of the gameobject.
    /// </summary>
    private void Start()
    {
        OGSize = transform.localScale;
    }

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
            this.gameObject.GetComponent<Lean.Touch.LeanScale>().enabled = true;
            this.gameObject.GetComponent<Lean.Touch.LeanRotate>().enabled = true;
            this.gameObject.GetComponent<AnimatorManager>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<Lean.Touch.LeanScale>().enabled = false;
            this.gameObject.GetComponent<Lean.Touch.LeanRotate>().enabled = false;

            this.gameObject.GetComponent<AnimatorManager>().enabled = true;
        }
    }
    
    /// <summary>
    /// 1) Detects the double taps (based on if the time between the two taps is smaller than the maximum times) in order to switch between movement and animation change mode.
    /// 2) Detects swipes based off the center touch and the radius of the movement of the finger.
    /// 3) If the user swipes than the boolean from the array is changed.
    /// </summary>
    internal void SwipeDetect()
    {
        Vector3 velocity = Vector3.zero;

        if(Input.touchCount == 0 && !Input.GetMouseButton(0))
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, OGSize, ref velocity, 0.1f);
        }

        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {
                newTime = Time.time + maxDoubleTapTime;
            }

            else if (TapCount == 2 && Time.time <= newTime)
            {
                if (changeAnimationMode == true)
                {
                    changeAnimationMode = false;
                }
                else
                {
                    changeAnimationMode = true;
                }
            }

        }
        if (Time.time > newTime)
        {
            TapCount = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            controls[i] = false;
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                controls[0] = true;
                startPos = Input.touches[0].position;
                startPos = Input.mousePosition;
                swiping = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled || Input.GetMouseButtonUp(0))
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
                        controls[2] = true;
                    }

                    else
                    {
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

    /// <summary>
    /// Resets the position of the input of the finger when released.
    /// </summary>
    internal void Reset()
    {
        swiping = false;
        startPos = Vector2.zero;
        swipeDistance = Vector2.zero;
    }
}