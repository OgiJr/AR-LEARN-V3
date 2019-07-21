using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModelsFixed : MonoBehaviour
{
<<<<<<< HEAD
    //public Material defaultMat;

    //void Update()
    //{

    //    if (GameObject.Find("Lesson2(Clone)"))
    //    {
    //        GameObject.Find("Lesson2(Clone)").transform.eulerAngles = new Vector3(0, 180, 0);
    //    }
    //    if (GameObject.Find("Basic_&(Clone)"))
    //    {
    //        GameObject.Find("Basic_&(Clone)").transform.position = new Vector3(200, 0, 1560);
    //    }
    //    if (GameObject.Find("Shelling&Ribs_&(Clone)"))
    //    {
    //        GameObject.Find("Shelling&Ribs_&(Clone)").transform.GetChild(0).transform.eulerAngles = new Vector3(-180, -90, -90);
    //    }
    //    if (GameObject.Find("Thin_Features_&(Clone)"))
    //    {
    //        GameObject.Find("Thin_Features_&(Clone)").transform.GetChild(0).transform.SetParent(GameObject.Find("Thin_Features_&(Clone) Position").transform);
    //        GameObject.Find("Thin_Features_&(Clone) Position").transform.GetChild(3).gameObject.GetComponent<Renderer>().enabled = true;
    //        GameObject.Find("Thin_Features_&(Clone) Position").transform.GetChild(3).transform.position = new Vector3(1, 0, 60);
    //        GameObject.Find("Thin_Features_&(Clone) Position").transform.GetChild(3).transform.eulerAngles = new Vector3(0, -300, 0);
    //        GameObject.Find("Thin_Features_&(Clone) Position").transform.GetChild(3).transform.localScale = new Vector3(10f, 10f, 10f);
    //    }
    //    if(GameObject.Find("Editing CS_&(Clone)"))
    //    {
    //        GameObject.Find("Editing CS_&(Clone)").transform.localEulerAngles = new Vector3(90, 0, 0);
    //        GameObject.Find("Editing CS_&(Clone)").transform.GetChild(0).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    //        GameObject.Find("Editing CS_&(Clone)").transform.GetChild(0).transform.localPosition = new Vector3(0.008f, -0.005f, 0.01f);

    //    }
    //    if (GameObject.Find("Partial_Editing CS_&(Clone)"))
    //    {
    //        GameObject.Find("Partial_Editing CS_&(Clone)").transform.position = new Vector3(-350, -195, 1600);
    //        GameObject.Find("Partial_Editing CS_&(Clone)").transform.eulerAngles = new Vector3(180, 0, -180);
    //    }
    //    if(GameObject.Find("Perforation Area Pattern&(Clone)"))
    //    {
    //        GameObject.Find("Perforation Area Pattern&(Clone)").transform.GetChild(0).transform.SetParent(GameObject.Find("Perforation Area Pattern&(Clone) Position").transform);
    //        GameObject.Find("Perforation Area Pattern&(Clone) Position").transform.GetChild(3).gameObject.GetComponent<Renderer>().enabled = true;
    //        GameObject.Find("Perforation Area Pattern&(Clone) Position").transform.GetChild(3).gameObject.GetComponent<Transform>().localScale = new Vector3(300, 300, 300);
    //        GameObject.Find("Perforation Area Pattern&(Clone) Position").transform.GetChild(3).gameObject.GetComponent<Transform>().eulerAngles = Vector3.zero;
    //        GameObject.Find("Perforation Area Pattern&(Clone) Position").transform.GetChild(3).gameObject.GetComponent<Transform>().position = new Vector3(0, -750, 0);
    //    }
    //    if (GameObject.Find("Circular_Pattern_&(Clone)"))
    //    {
    //        GameObject.Find("Circular_Pattern_&(Clone)").transform.GetChild(0).transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    //    }
    //    if (GameObject.Find("Lesson2(Clone)"))
    //    {
    //        GameObject.Find("Lesson2(Clone)").transform.GetChild(0).localScale = new Vector3(0.6f, 0.6f, 0.6f);
    //        GameObject.Find("Lesson2(Clone)").transform.GetChild(0).eulerAngles = new Vector3(90, 0, 0);
    //        GameObject.Find("Lesson2(Clone)").transform.GetChild(0).gameObject.GetComponent<Renderer>().material = defaultMat;
    //    }
    //    if (GameObject.Find("Mirror_Body_&(Clone)"))
    //    {
    //        GameObject.Find("Mirror_Body_&(Clone)").transform.localEulerAngles = new Vector3(-120, 0, -180);
    //        GameObject.Find("Mirror_Body_&(Clone)").transform.localScale = new Vector3(50, 50, 50);
    //    }
    //    if(GameObject.Find("Ratchet Body_&(Clone)"))
    //    {
    //        GameObject.Find("Ratchet Body_&(Clone)").transform.GetChild(0).localScale = new Vector3(0.2f, 0.2f, 0.2f);
    //    }
    //    if(GameObject.Find("Partial_Editing CS_&(Clone)"))
    //    {
    //        GameObject.Find("Partial_Editing CS_&(Clone)").transform.GetChild(0).localScale = new Vector3(2f, 2f, 2f);
    //    }
    //    if (GameObject.Find("WorkingConfigs_&(Clone)"))
    //    {
    //        GameObject.Find("WorkingConfigs_&(Clone)").transform.GetChild(0).localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //    }
    //}
=======
    void Update()
    {
        if (GameObject.Find("Programmable(Clone)"))
        {
            GameObject.Find("Programmable(Clone)").transform.GetChild(1).transform.localPosition = Vector3.zero;
            GameObject.Find("Programmable(Clone)").transform.GetChild(1).transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
        if (GameObject.Find("Circulatory_System(Clone)"))
        {
            Destroy(GameObject.Find("Circulatory_System(Clone)").GetComponent<Animator>());
            GameObject.Find("Circulatory_System(Clone)").transform.localEulerAngles = new Vector3(-90, 90, 90);
        }
        if (GameObject.Find("Programmable(Clone)")){
            GameObject.Find("Programmable(Clone)").transform.localPosition = new Vector3(15, -50, -100);
        }
        if (GameObject.Find("PlantCell(Clone)"))
        {
            GameObject.Find("PlantCell(Clone)").transform.localPosition = new Vector3(20, -25, 80);
        }
        if (GameObject.Find("Earth_Orbit(Clone)"))
        {
            Destroy(GameObject.Find("Earth_Orbit(Clone)").GetComponent<SizeAdjust>());
            GameObject.Find("Earth_Orbit(Clone)").transform.localScale = new Vector3(20, 20, 20);
            Destroy(GameObject.Find("Earth_Orbit(Clone)").transform.GetChild(1).gameObject);
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
>>>>>>> b4717e9a396edab719463fcae26f8f568b599962
}
