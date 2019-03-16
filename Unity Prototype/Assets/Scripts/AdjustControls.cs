using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustControls : MonoBehaviour
{
    private SwipeControls swipeControls;

    private void Update()
    {
<<<<<<< HEAD

=======
>>>>>>> f658a2ea6b44743dbdcb32d1334c0903b6b8351f
        if (swipeControls == null)
        {
            swipeControls = this.gameObject.GetComponent<SwipeControls>();
        }

<<<<<<< HEAD
        else if (Mathf.Abs(swipeControls.swipeDistance.y) > 100 && Mathf.Abs(swipeControls.swipeDistance.y) > Mathf.Abs(swipeControls.swipeDistance.x))
        {
            transform.localScale += new Vector3(swipeControls.swipeDistance.y,swipeControls.swipeDistance.y,swipeControls.swipeDistance.y);
            Debug.Log(swipeControls.swipeDistance.y);
        }

        Debug.Log(swipeControls.swipeDistance.y);

        if (Mathf.Abs(swipeControls.swipeDistance.x) > 100 && Mathf.Abs(swipeControls.swipeDistance.x) > Mathf.Abs(swipeControls.swipeDistance.y))
=======
        else if (Mathf.Abs(swipeControls.swipeDistance.y) > 0 && Mathf.Abs(swipeControls.swipeDistance.y) > Mathf.Abs(swipeControls.swipeDistance.x))
        {
            transform.localScale += new Vector3(swipeControls.swipeDistance.y,swipeControls.swipeDistance.y,swipeControls.swipeDistance.y);
        }

        if (Mathf.Abs(swipeControls.swipeDistance.x) > 0 && Mathf.Abs(swipeControls.swipeDistance.x) > Mathf.Abs(swipeControls.swipeDistance.y))
>>>>>>> f658a2ea6b44743dbdcb32d1334c0903b6b8351f
        {
            transform.localEulerAngles += new Vector3(0, -swipeControls.swipeDistance.x, 0);
        }
    }
}