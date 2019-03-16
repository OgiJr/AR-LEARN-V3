using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingPlanets : MonoBehaviour
{
    int state = 0;

    private SwipeControls swipeControls;
    private GameObject addText;

    private void Update()
    {
        if (addText == null)
        {
            addText = GameObject.Find("AddText");
        }

        if (swipeControls == null)
        {
            swipeControls = this.gameObject.GetComponent<SwipeControls>();
        }

        if (swipeControls.controls[2] || Input.GetKey(KeyCode.A))
        {
            state++;
            ChangeState();
        }

        if (swipeControls.controls[1])
        {
            state--;
            ChangeState();
        }

        if (state == 0)
        {
            state = 1;
            ChangeState();
        }

        if (state < 1)
        {
            state = 8;
        }

        else if (state > 8)
        {
            state = 1;
            ChangeState();
        }

        this.transform.localScale = Vector3.one;
    }

    void ChangeState()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (state != 0)
        {
            Destroy(gameObject.GetComponent<Renderer>());
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        else
        {
            if (gameObject.GetComponent<Renderer>() != null)
            {
                gameObject.AddComponent<Renderer>();
            }
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (state == 1)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Mercury";
        }
        if (state == 2)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Mars";
        }
        if (state == 3)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Venus";
        }
        if (state == 4)
        {
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Earth";
        }
        if (state == 5)
        {
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Jupiter";
        }
        if (state == 6)
        {
            gameObject.transform.GetChild(7).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Saturn";
        }
        if (state == 7)
        {
            gameObject.transform.GetChild(8).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Neptune";
        }
        if (state == 8)
        {
            gameObject.transform.GetChild(9).gameObject.SetActive(true);
            addText.gameObject.GetComponent<Text>().text = "Uranus";

        }
    }

}