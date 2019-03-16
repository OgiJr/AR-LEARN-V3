using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraManager : MonoBehaviour
{
    public Material space;

    private void Update()
    {
        if(RenderSettings.skybox != space)
        {
            RenderSettings.skybox = space;
        }
    }
}
