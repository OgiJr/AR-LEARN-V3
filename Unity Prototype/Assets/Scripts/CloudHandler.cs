using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.PostProcessing;

/// <summary>
/// Main vuforia handler. It takes information from the vuforia API and it augments it into the camera.
/// </summary>
public class CloudHandler : MonoBehaviour, ICloudRecoEventHandler
{
    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";

    private AnimatorManager animatorManager;
    private SwipeControls swipeControls;
    public ImageTargetBehaviour imageTargetTemplate;

    public GameObject exitButton;
    public GameObject scanButton;
    internal GameObject selectedObject;
    public GameObject errorUI;
    public ServerDownloader svDownloader;
    public GameObject infoTextGO;

    private bool changeable = true;
    private bool errorShown = false;
    public RuntimeAnimatorController exitAnimator;
    public UIEffects uiEffects;

    private GameObject follow;
    private bool instantiated = false;
    private bool detected = false;
    GameObject newImageTarget = null;

    public Material transparent;

    string name = "initial";

    /// <summary>
    /// Register this event handler at the cloud reco behaviour.
    /// </summary>
    void Start()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

        if (mCloudRecoBehaviour)
        {
            mCloudRecoBehaviour.RegisterEventHandler(this);
        }
    }

    #region SwitchMode
    private void Update()
    {
        if (mIsScanning == true && exitButton.activeSelf == false)
        {
            uiEffects.Exit();
        }
        else if (mIsScanning == false && scanButton.activeSelf == false)
        {
            uiEffects.Scan();
        }

        mCloudRecoBehaviour.CloudRecoEnabled = mIsScanning;

        if (follow == null && instantiated == true)
        {
            follow = GameObject.Find(selectedObject.name + " Position");
        }

        if (instantiated == true)
        {
            newImageTarget.transform.localScale = new Vector3(20f, 20f, 20f);

        //    if (follow.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled == false)
        //    {
        //        if (selectedObject.GetComponent<Renderer>() != null)
        //        {
        //            selectedObject.GetComponent<Renderer>().enabled = false;
        //        }
        //        if (selectedObject.GetComponentInChildren<Renderer>() != null)
        //        {
        //            foreach (Renderer renderer in selectedObject.GetComponentsInChildren<Renderer>())
        //            {
        //                renderer.enabled = false;
        //            }
        //        }
        //        if (selectedObject.gameObject.GetComponent<Canvas>() != null)
        //        {
        //            selectedObject.gameObject.GetComponent<Canvas>().enabled = false;
        //        }
        //        if (selectedObject.transform.childCount > 0)
        //        {
        //            if (selectedObject.transform.GetChild(0).GetComponentInChildren<Renderer>() == true)
        //            {
        //                foreach (Renderer renderer in selectedObject.transform.GetChild(0).GetComponentsInChildren<Renderer>())
        //                {
        //                    renderer.enabled = false;
        //                }
        //            }
        //        }
        //        if (selectedObject.GetComponentInChildren<Canvas>() != null)
        //        {
        //            foreach (Canvas canvas in selectedObject.GetComponentsInChildren<Canvas>())
        //            {
        //                canvas.enabled = false;
        //            }
        //        }

        //        if (selectedObject.GetComponentInChildren<AudioSource>() != null)
        //        {
        //            foreach (AudioSource audio in selectedObject.GetComponentsInChildren<AudioSource>())
        //            {
        //                audio.enabled = false;
        //            }
        //        }
        //    }


        //    else
        //    {
        //        if (selectedObject != null)
        //        {
        //            if (selectedObject.GetComponent<Renderer>() != null)
        //            {
        //                selectedObject.GetComponent<Renderer>().enabled = true;
        //            }
        //            if (selectedObject.GetComponent<Canvas>() != null)
        //            {
        //                selectedObject.GetComponent<Canvas>().enabled = true;
        //            }
        //            if (selectedObject.GetComponentInChildren<Renderer>() != null)
        //            {
        //                foreach (Renderer renderer in selectedObject.GetComponentsInChildren<Renderer>())
        //                {
        //                    renderer.enabled = true;
        //                }
        //            }
        //            if (selectedObject.transform.childCount > 0)
        //            {
        //                if (selectedObject.transform.GetChild(0).GetComponentInChildren<Renderer>() == true)
        //                {
        //                    foreach (Renderer renderer in selectedObject.transform.GetChild(0).GetComponentsInChildren<Renderer>())
        //                    {
        //                        renderer.enabled = true;
        //                    }
        //                }
        //            }
        //            if (selectedObject.GetComponentInChildren<Canvas>() != null)
        //            {
        //                foreach (Canvas canvas in selectedObject.transform.GetComponentsInChildren<Canvas>())
        //                {
        //                    canvas.enabled = true;
        //                }
        //            }

        //            if (selectedObject.GetComponentInChildren<AudioSource>() != null)
        //            {
        //                foreach (AudioSource audio in selectedObject.GetComponentsInChildren<AudioSource>())
        //                {
        //                    audio.enabled = true;
        //                }
        //            }
        //        }
        //    }
        }
    }

    public void ScanningOn()
    {
        mIsScanning = true;
    }

    public void ScanningOff()
    {
        mIsScanning = false;
        if (selectedObject != null)
        {
            Destroy(selectedObject);
            Destroy(newImageTarget);
            instantiated = false;
        }

        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.name.Contains("Position"))
            {
                Destroy(go);
            }
        }

        if (Camera.main.gameObject.GetComponent<PostProcessingBehaviour>() != null)
        {
            Camera.main.gameObject.GetComponent<PostProcessingBehaviour>().enabled = false;
        }
        Text infoText = infoTextGO.GetComponent<Text>() as Text;
        infoText.text = "Please select a 3D model so that we can desplay its information.";
    }
    #endregion

    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized");
    }
    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }
    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning)
    {
        var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        tracker.TargetFinder.ClearTrackables(false);
    }

    /// <summary>
    /// The main manager for augmeting objects.
    /// </summary>
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        if (targetSearchResult.TargetName != name)
        {
            name = targetSearchResult.TargetName;

            if (selectedObject != null)
            {
                selectedObject = null;
                newImageTarget = null;
            }

            if (newImageTarget == null)
            {
                newImageTarget = Instantiate(imageTargetTemplate.gameObject) as GameObject;
                newImageTarget.transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = true;
            }

            for (int i = 0; i < svDownloader.p.models; i++)
            {

                if (svDownloader.p.bundle[i].name == targetSearchResult.TargetName)
                {
                    Debug.Log("az");
                    GameObject augmentation = null;

                    Text infoText = infoTextGO.GetComponent<Text>() as Text;
                    infoText.text = svDownloader.p.text[i];

                    /// <summary>
                    /// Instatitates the object from the resources folder after detecting the GameObject. Then it changes the name so that we can find the game object in the scene.
                    /// </summary>     
                    #region Instantiate 
                    if (svDownloader.p.bundle[i].mainAsset != null)
                    {
                        selectedObject = Instantiate(svDownloader.p.bundle[i].mainAsset as GameObject);
                    }
                    else
                    {
                        selectedObject = GameObject.Find("ExampleObject");
                    }
                    #endregion

                    //Fixes all errors that may relate to rendering.
                    #region Rendering Issues
                    if (selectedObject.GetComponent<Renderer>() != null)
                    {
                        selectedObject.GetComponent<Renderer>().enabled = true;
                    }

                    if (selectedObject.GetComponentsInChildren<Renderer>() != null)
                    {
                        foreach (Renderer rend in selectedObject.GetComponentsInChildren<Renderer>())
                        {
                            rend.enabled = true;
                        }
                    }

                    if (newImageTarget.GetComponent<RectTransform>() != null)
                    {
                        Destroy(newImageTarget.GetComponent<RectTransform>());
                    }
                    if (selectedObject.GetComponentsInChildren<Renderer>() != null)
                    {
                        foreach (Renderer mat in selectedObject.GetComponentsInChildren<Renderer>())
                        {
                            mat.material.shader = Shader.Find("Standard");
                        }
                    }
                    if (selectedObject.GetComponent<Renderer>() != null)
                    {
                        selectedObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                    }
                    #endregion

                    //Standardizes all objects (their scale, position and rotation)
                    #region Transform
                    selectedObject.transform.parent = newImageTarget.transform;

                    newImageTarget.transform.name = selectedObject.name + " Position";

                    selectedObject.transform.localEulerAngles = Vector3.zero;
                    newImageTarget.transform.position = Vector3.zero;
                    selectedObject.transform.localPosition = Vector3.zero;

                    Bounds current = new Bounds();
                    Bounds desired = imageTargetTemplate.gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().bounds;
                    Vector3 scale = new Vector3();

                    if (selectedObject.GetComponent<Renderer>() != null)
                    {
                        current = selectedObject.GetComponent<Renderer>().bounds;
                    }

                    if (selectedObject.transform.childCount > 0)
                    {
                        if (selectedObject.GetComponentsInChildren<Renderer>() != null)
                        {
                            foreach (Renderer r in selectedObject.GetComponentsInChildren<Renderer>())
                            {
                                current.Encapsulate(r.bounds);
                            }
                        }
                    }

                    scale = new Vector3(current.size.x / desired.size.x, current.size.y / desired.size.y, current.size.z / desired.size.z);
                    selectedObject.transform.localScale = scale;
                    selectedObject.transform.localScale = selectedObject.transform.localScale * 50000;
                    #endregion

                    /// <summary>
                    /// Loads and adds all of the components to the instantiated object so that you can scale it, rotate it and change its animations.
                    /// </summary>
                    #region Controls

                    selectedObject.AddComponent<Lean.Touch.LeanRotate>();
                    selectedObject.AddComponent<Lean.Touch.LeanScale>();

                    if (selectedObject.GetComponent<AnimatorManager>() == null)
                    {
                        animatorManager = selectedObject.AddComponent<AnimatorManager>();
                    }

                    else
                    {
                        animatorManager = selectedObject.GetComponent<AnimatorManager>();
                    }

                    if (selectedObject.GetComponent<SwipeControls>() == null)
                    {
                        swipeControls = selectedObject.AddComponent<SwipeControls>();
                    }

                    else
                    {
                        swipeControls = selectedObject.GetComponent<SwipeControls>();
                    }
                    #endregion

                    if (augmentation != null)
                    {
                        augmentation.transform.SetParent(newImageTarget.transform);
                    }
                    if (imageTargetTemplate)
                    {
                        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                        ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour)tracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget);
                    }
                    else if (errorShown == false)
                    {
                        errorShown = true;
                        errorUI.SetActive(true);
                        Invoke("ErrorBack", 5);
                    }

                    instantiated = true;
                    detected = true;
                }
            }
        }
    }

    /// <summary>
    /// Error out if vuforia crashes.
    /// </summary>
    private void ErrorBack()
    {
        errorUI.SetActive(false);
    }
}