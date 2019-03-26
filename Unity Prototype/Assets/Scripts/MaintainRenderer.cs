using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRenderer : MonoBehaviour
{
    void Update()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;       
    }
}
