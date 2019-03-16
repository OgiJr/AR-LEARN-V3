using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAdjust : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
}
