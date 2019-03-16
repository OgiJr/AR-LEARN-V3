using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammableScript : MonoBehaviour
{
    public void One()
    {
        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void Two()
    {
        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void Three()
    {
        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}