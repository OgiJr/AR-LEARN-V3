using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModelsFixed : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Earth_Orbit(Clone)"))
        {
            Destroy(GameObject.Find("Earth_Orbit(Clone)").GetComponent<SizeAdjust>());
            GameObject.Find("Earth_Orbit(Clone)").transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(1.02f, 1.02f, 1.02f);
        }
        if (GameObject.Find("PlantCell(Clone)"))
        {
            GameObject.Find("PlantCell(Clone)").transform.localPosition = new Vector3(25, 0, 150);
        }
    }
}
