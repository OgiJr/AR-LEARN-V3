using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustControls : MonoBehaviour
{
    private SwipeControls swipeControls;

    private void Update()
    {
        if (Mathf.Abs(swipeControls.swipeDistance.y) > 50 && Mathf.Abs(swipeControls.swipeDistance.y) > Mathf.Abs(swipeControls.swipeDistance.x))
        {
            transform.position += new Vector3(0, 0, swipeControls.swipeDistance.y/200);
        }

        else if (Mathf.Abs(swipeControls.swipeDistance.x) > 50 && Mathf.Abs(swipeControls.swipeDistance.x) > Mathf.Abs(swipeControls.swipeDistance.y))
        {
            transform.localEulerAngles += new Vector3(0, -swipeControls.swipeDistance.x, 0);
        }
    }
}