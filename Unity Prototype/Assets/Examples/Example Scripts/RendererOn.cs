using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererOn : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.GetComponent<Renderer>().enabled = true;
    }
}
