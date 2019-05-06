using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ARVideoPlayer : MonoBehaviour
{
    private void Update()
    {
        if (GameObject.FindObjectsOfType<VideoPlayer>() != null)
        {
            foreach (VideoPlayer vp in GameObject.FindObjectsOfType<VideoPlayer>())
            {
                if (vp.targetCamera == null)
                {
                    vp.targetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                }
            }
        }
    }
}
