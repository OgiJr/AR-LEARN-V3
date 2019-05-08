using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject newImageTarget;
    public GameObject selectedObject;
    private void Update()
    {
        Bounds bounds = selectedObject.GetComponent<Renderer>().bounds;
        Bounds exampleBounds = newImageTarget.transform.GetChild(1).gameObject.GetComponent<Renderer>().bounds;

        if (selectedObject.GetComponent<MeshRenderer>() == null)
        {
            selectedObject.AddComponent<MeshRenderer>();
        }

        if (selectedObject.GetComponent<MeshFilter>() == null)
        {
            selectedObject.AddComponent<MeshFilter>();
            selectedObject.GetComponent<MeshFilter>().mesh = newImageTarget.transform.GetChild(1).gameObject.GetComponent<MeshFilter>().mesh;
        }

        foreach (Renderer rend in selectedObject.GetComponentsInChildren<Renderer>())
        {
            if (selectedObject.GetComponent<Renderer>() != rend)
            {
                bounds.Encapsulate(rend.bounds);
                Debug.Log(bounds.size);
            }
        }

        Vector3 obj1_size = bounds.max - bounds.min;
        Vector3 obj2_size = exampleBounds.max - exampleBounds.min;

        selectedObject.transform.localScale = selectedObject.transform.localScale * (componentMax(newImageTarget.transform.GetChild(1).transform.localScale) / componentMax(obj1_size));
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