using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSize : MonoBehaviour
{
    public GameObject obj1, obj2;

    // The normalized size that both objects will be
    public Vector3 size = Vector3.one;

    // If true, the objects are scaled uniformly.  If false, scale is per-component
    public bool preserveDimensions = true;

    void Start()
    {
        if (this.gameObject.GetComponent<MeshRenderer>() == null)
        {
            this.gameObject.AddComponent<MeshRenderer>();
        }
        if(this.gameObject.GetComponent<MeshFilter>() == null)
        {
            this.gameObject.AddComponent<MeshFilter>();
            this.gameObject.GetComponent<MeshFilter>().mesh = obj2.GetComponent<MeshFilter>().mesh;
        }
        Bounds bounds = this.gameObject.GetComponent<Renderer>().bounds;
        Bounds b = obj2.GetComponent<Renderer>().bounds;

        foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (gameObject.GetComponent<Renderer>() != rend)
            {
                bounds.Encapsulate(rend.bounds);
                Debug.Log(rend.bounds);
            }
        }

        Vector3 obj1_size = bounds.max - bounds.min;
        Vector3 obj2_size = b.max - b.min;

            obj1.transform.localScale = obj1.transform.localScale * (componentMax(size) / componentMax(obj1_size));
            obj2.transform.localScale = obj2.transform.localScale * (componentMax(size) / componentMax(obj2_size));
    }

    float componentMax(Vector3 a)
    {
        return Mathf.Max(Mathf.Max(a.x, a.y), a.z);
    }

    Vector3 div(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
}