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
        if (GameObject.Find("Programmable(Clone)"))
        {
            GameObject.Find("Programmable(Clone)").transform.localPosition = new Vector3(13, 0, -100);
            GameObject.Find("Programmable(Clone)").transform.GetChild(1).transform.localPosition = new Vector3(-.4f,-1.2f,3.6f);
            GameObject.Find("Programmable(Clone)").transform.GetChild(1).transform.localScale = new Vector3(.5f, .5f, .5f);
        }
        if (GameObject.Find("Lesson2(Clone)"))
        {
            GameObject.Find("Lesson2(Clone)").transform.GetChild(0).transform.localScale = new Vector3(7, 7, 7);
            GameObject.Find("Lesson2(Clone)").transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
