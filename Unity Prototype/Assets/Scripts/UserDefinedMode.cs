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

    public ServerDownloader serverDownloader;

    private void Start()
    {
        serverDownloader.getInfo(PlayerPrefs.GetString("ID"));
        serverDownloader.downloadModels();
    }

    /// <summary>
    /// Same method with resources as the CloudHandler(Open the documentation from that script for more details) but here you select the name of the gameobject and it is instantiated when you find the perfect image and press the button.
    /// </summary>
    private void Update()
    {
        if (serverDownloader != null)
        {
            serverDownloader = GameObject.Find("ServerDownloader").GetComponent<ServerDownloader>();
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

        if (objectName == string.Empty)
        {
            Debug.Log("Please select an object");
        }

        #region SetString
        objectName = inputText.GetComponent<TMPro.TMP_InputField>().text;
        Debug.Log(objectName);
        #endregion

        if (lastObjName != objectName)
        {
            Destroy(augmentationObject);
            lastObjName = objectName;
        }

        lastObjName = objectName;

        foreach (AssetBundle ab in serverDownloader.p.bundle)
        {
            if (augmentationObject == null && ab.name == objectName)
            {
                Debug.Log("fuckingcunt123");
                augmentationObject = Instantiate(ab.mainAsset) as GameObject;
            }
            else if (showedError == false)
            {
                errorUI.SetActive(true);
                showedError = true;
                Invoke("RemoveErrorScreen", 3);
            }
        }

        Vector3 sizeCalculated = userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size;
        userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

        userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
        userDefinedTarget.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

        augmentationObject.transform.parent = userDefinedTarget.transform;
        augmentationObject.transform.position = Vector3.zero;
        augmentationObject.transform.eulerAngles = Vector3.zero;
        augmentationObject.transform.localScale = sizeCalculated;

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
}