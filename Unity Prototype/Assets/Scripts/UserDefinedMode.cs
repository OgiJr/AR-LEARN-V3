using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
///  Handles the user defined mode.
/// </summary>
public class UserDefinedMode : MonoBehaviour
{
    public string objectName = "";
    private string lastObjName;

    internal GameObject augmentationObject;
    public GameObject userDefinedTarget;
    public GameObject inputText;

    private AnimatorManager animatorManager;
    private SwipeControls swipeControls;
    private UDTEventHandler udtEventHandler;

    private bool turnedOff = false;
    private bool showedError = false;
    private GameObject errorUI;

    public UDTLoader serverDownloader;
    public Material transparent;
    /// <summary>
    /// Same method with resources as the CloudHandler(Open the documentation from that script for more details) but here you select the name of the gameobject and it is instantiated when you find the perfect image and press the button.
    /// </summary>
    private void Update()
    {
        if (serverDownloader != null)
        {
            serverDownloader = GameObject.Find("UDTLoader").GetComponent<UDTLoader>();
        }

        if (errorUI == null)
        {
            errorUI = GameObject.Find("ErrorUI");
            errorUI.SetActive(false);
        }

        if (udtEventHandler == null)
        {
            udtEventHandler = this.gameObject.GetComponent<UDTEventHandler>();
        }

        #region SetString
        objectName = PlayerPrefs.GetString("UDTID");
        #endregion

        if (lastObjName != objectName)
        {
            Destroy(augmentationObject);
            lastObjName = objectName;
        }

        lastObjName = objectName;

        if (augmentationObject == null)
        {
            augmentationObject = (GameObject)Instantiate(serverDownloader.load(PlayerPrefs.GetString("UDTID")));
        }
        userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

        userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
        userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

        augmentationObject.transform.parent = userDefinedTarget.transform;
        augmentationObject.transform.position = Vector3.zero;
        augmentationObject.transform.eulerAngles = Vector3.zero;

        if (augmentationObject.transform.childCount > 1)
        {
            bool noRend = false;

            if (augmentationObject.GetComponent<MeshRenderer>() == null)
            {
                augmentationObject.AddComponent<MeshRenderer>();
                noRend = true;
            }
            else
            {
                noRend = false;
            }

            if (augmentationObject.GetComponent<MeshFilter>() == null)
            {
                augmentationObject.AddComponent<MeshFilter>();
                augmentationObject.GetComponent<MeshFilter>().mesh = userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh;
                noRend = true;
            }
            else { noRend = false; }

            augmentationObject.transform.localScale = Vector3.one;

            Bounds bounds = augmentationObject.GetComponent<Renderer>().bounds;
            Bounds b = userDefinedTarget.transform.GetChild(0).GetComponent<Renderer>().bounds;

            foreach (Renderer rend in augmentationObject.GetComponentsInChildren<Renderer>())
            {
                if (augmentationObject.GetComponent<Renderer>() != rend)
                {
                    bounds.Encapsulate(rend.bounds);
                }
            }

            Vector3 obj1_size = bounds.max - bounds.min;
            Vector3 obj2_size = b.max - b.min;
            Vector3 size = new Vector3(1, 1, 1);

            augmentationObject.transform.localScale = augmentationObject.transform.localScale * (componentMax(size) / componentMax(obj1_size));
            userDefinedTarget.transform.GetChild(0).transform.localScale = userDefinedTarget.transform.GetChild(0).transform.localScale * (componentMax(size) / componentMax(obj2_size));

            if (noRend == true)
            {
                Debug.Log("ben shapiro");
                augmentationObject.GetComponent<Renderer>().material = transparent;
            }
        }
        else
        {
            augmentationObject.GetComponent<Transform>().localScale = userDefinedTarget.transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        }

        if (augmentationObject.GetComponent<SizeAdjust>() == null)
        {
            augmentationObject.AddComponent<SizeAdjust>();
        }

        if (augmentationObject.GetComponent<Lean.Touch.LeanScale>() == null)
        {
            augmentationObject.AddComponent<Lean.Touch.LeanScale>();
            augmentationObject.AddComponent<Lean.Touch.LeanRotate>();
        }

        #region Singleton
        if (augmentationObject.GetComponent<AnimatorManager>() == null)
        {
            animatorManager = augmentationObject.AddComponent<AnimatorManager>();
        }

        else
        {
            animatorManager = augmentationObject.GetComponent<AnimatorManager>();
        }
        if (turnedOff == false)
        {
            if (augmentationObject.GetComponent<Renderer>() != null)
            {
                augmentationObject.GetComponent<Renderer>().enabled = false;
            }
            if (augmentationObject.transform.childCount > 0)
            {
                if (augmentationObject.transform.GetComponentInChildren<Renderer>() != null)
                {
                    augmentationObject.transform.GetComponentInChildren<Renderer>().enabled = false;
                    foreach (Renderer renderer in userDefinedTarget.transform.GetChild(1).gameObject.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = false;
                    }
                }
            }
            turnedOff = true;
        }

        if (augmentationObject.GetComponent<SwipeControls>() == null)
        {
            swipeControls = augmentationObject.AddComponent<SwipeControls>();
        }

        else
        {
            swipeControls = augmentationObject.GetComponent<SwipeControls>();
        }
        #endregion
    }

    private void RemoveErrorScreen()
    {
        errorUI.SetActive(false);
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