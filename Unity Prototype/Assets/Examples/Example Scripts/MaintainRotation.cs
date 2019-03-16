using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRotation : MonoBehaviour
{
    public Vector3 rotation = new Vector3(-90, 0, 0);

    void Update()
    {
        this.gameObject.transform.eulerAngles = rotation;
    }
}
