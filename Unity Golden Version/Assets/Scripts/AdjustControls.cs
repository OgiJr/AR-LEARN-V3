using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustControls : MonoBehaviour
{
    private SwipeControls swipeControls;

    private void Update()
    {
        if (swipeControls == null)
        {
            swipeControls = this.gameObject.GetComponent<SwipeControls>();
        }

        swipeControls.SwipeDetect();

        //if (Input.GetKey(KeyCode.A))
        //{
        //    swipeControls.changeAnimationMode = false;
        //}

        if (swipeControls.changeAnimationMode == false)
        {
            //gameObject.GetComponent<AnimatorManager>().enabled = false;
            //gameObject.GetComponent<Animator>().enabled = false;

            //if (Mathf.Abs(swipeControls.swipeDistance.x) > 0 && Mathf.Abs(swipeControls.swipeDistance.x) > Mathf.Abs(swipeControls.swipeDistance.y))
            //{
            //    transform.localEulerAngles += new Vector3(0, -swipeControls.swipeDistance.x / 1200, 0);
            //}
            //else if (Mathf.Abs(swipeControls.swipeDistance.y) > 0 && Mathf.Abs(swipeControls.swipeDistance.x) < Mathf.Abs(swipeControls.swipeDistance.y))
            //{
            //    transform.localEulerAngles += new Vector3(-swipeControls.swipeDistance.y / 1200, 0, 0);
            //}
        }
    }
}
