using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSun : MonoBehaviour
{
    Vector3 originalPos;
    public GameObject center;

    private void Start()
    {
        originalPos = this.transform.position;
    }

    private void Update()
    {
        transform.position = center.transform.position + originalPos;
    }
}
