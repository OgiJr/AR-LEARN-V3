using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModelsFixed : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Circulatory_System(Clone)"))
        {
            Destroy(GameObject.Find("Circulatory_System(Clone)").GetComponent<Animator>());
            GameObject.Find("Circulatory_System(Clone)").transform.localEulerAngles = new Vector3(-90, 90, 90);
        }
        if (GameObject.Find("Lesson2(Clone)"))
        {
            GameObject.Find("Lesson2(Clone)").transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (GameObject.Find("Basic_&(Clone)"))
        {
            GameObject.Find("Basic_&(Clone)").transform.position = new Vector3(200, 0, 1560);
        }
        if (GameObject.Find("Shelling&Ribs_&(Clone)"))
        {
            GameObject.Find("Shelling&Ribs_&(Clone)").transform.GetChild(0).transform.eulerAngles = new Vector3(-180, -90, -90);
        }
        if (GameObject.Find("Thin_Features_&(Clone)"))
        {
            GameObject.Find("Thin_Features_ & (Clone)").transform.eulerAngles = Vector3.zero;
        }
        if(GameObject.Find("Editing CS_&(Clone)"))
        {
            GameObject.Find("Editing CS_&(Clone)").transform.localEulerAngles = new Vector3(90, 0, 0);
        }
        if(GameObject.Find("Partial_Editing CS_&(Clone)"))
        {
            GameObject.Find("Partial_Editing CS_&(Clone)").transform.position = new Vector3(-350, -195, 1600);
            GameObject.Find("Partial_Editing CS_&(Clone)").transform.eulerAngles = new Vector3(180, 0, -180);
        }
        if(GameObject.Find("Perforation Area Pattern&(Clone)"))
        {
            GameObject.Find("Perforation Area Pattern&(Clone)").transform.localEulerAngles = new Vector3(-90, 0, 0);
            GameObject.Find("Perforation Area Pattern&(Clone)").transform.localPosition = new Vector3(0, 0, 80);
        }
    }
}
