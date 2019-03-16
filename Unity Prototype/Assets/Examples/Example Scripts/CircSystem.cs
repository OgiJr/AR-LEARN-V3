using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircSystem : MonoBehaviour
{
    private GameObject addText;

    private void Update()
    {
        if(addText == null)
        {
            addText = GameObject.Find("AddText");
        }
    }
    void OnMouseDown()
    {
        Debug.Log("kur");
        addText.GetComponent<Text>().text = this.gameObject.name;
    }
}
