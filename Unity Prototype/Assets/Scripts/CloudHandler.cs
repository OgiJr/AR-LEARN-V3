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

            if (follow.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled == false)
            {
                if (selectedObject.GetComponent<Renderer>() != null)
                {
                    selectedObject.GetComponent<Renderer>().enabled = false;
                }
                if (selectedObject.GetComponentInChildren<Renderer>() != null)
                {
                    foreach (Renderer renderer in selectedObject.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = false;
                    }
                }
                if (selectedObject.gameObject.GetComponent<Canvas>() != null)
                {
                    selectedObject.gameObject.GetComponent<Canvas>().enabled = false;
                }
                if (selectedObject.transform.childCount > 0)
                {
                    if (selectedObject.transform.GetChild(0).GetComponentInChildren<Renderer>() == true)
                    {
                        foreach (Renderer renderer in selectedObject.transform.GetChild(0).GetComponentsInChildren<Renderer>())
                        {
                            renderer.enabled = false;
                        }
                    }
                }
                if (selectedObject.GetComponentInChildren<Canvas>() != null)
                {
                    foreach (Canvas canvas in selectedObject.GetComponentsInChildren<Canvas>())
                    {
                        canvas.enabled = false;
                    }
                }

                if (selectedObject.GetComponentInChildren<AudioSource>() != null)
                {
                    foreach (AudioSource audio in selectedObject.GetComponentsInChildren<AudioSource>())
                    {
                        audio.enabled = false;
                    }
                }
            }


            else
            {
                if (selectedObject.GetComponent<Renderer>() != null)
                {
                    selectedObject.GetComponent<Renderer>().enabled = true;
                }
                if (selectedObject.GetComponent<Canvas>() != null)
                {
                    selectedObject.GetComponent<Canvas>().enabled = true;
                }
                if (selectedObject.GetComponentInChildren<Renderer>() != null)
                {
                    foreach (Renderer renderer in selectedObject.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = true;
                    }
                }
                if (selectedObject.transform.childCount > 0)
                {
                    if (selectedObject.transform.GetChild(0).GetComponentInChildren<Renderer>() == true)
                    {
                        foreach (Renderer renderer in selectedObject.transform.GetChild(0).GetComponentsInChildren<Renderer>())
                        {
                            renderer.enabled = true;
                        }
                    }
                }
                if (selectedObject.GetComponentInChildren<Canvas>() != null)
                {
                    foreach (Canvas canvas in selectedObject.transform.GetComponentsInChildren<Canvas>())
                    {
                        canvas.enabled = true;
                    }
                }

                if (selectedObject.GetComponentInChildren<AudioSource>() != null)
                {
                    foreach (AudioSource audio in selectedObject.GetComponentsInChildren<AudioSource>())
                    {
                        audio.enabled = true;
                    }
                }
            }
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

        if (selectedObject != null)
        {
            Destroy(selectedObject);
            Destroy(newImageTarget);

            selectedObject = null;
            newImageTarget = null;
        }

        if (newImageTarget == null)
        {
            newImageTarget = Instantiate(imageTargetTemplate.gameObject) as GameObject;
        }

        for (int i = 0; i < svDownloader.p.models; i++)
        {
            if (svDownloader.p.bundle[i].name == targetSearchResult.TargetName)
            {
                GameObject augmentation = null;

                Text infoText = infoTextGO.GetComponent<Text>() as Text;
                infoText.text = svDownloader.p.text[i];
                Debug.Log(svDownloader.p.text[0]);

                /// <summary>
                /// Instatitates the object from the resources folder after detecting the GameObject. Then it changes the name so that we can find the game object in the scene.
                /// </summary>     

                selectedObject = Instantiate(svDownloader.p.bundle[i].mainAsset as GameObject);
                selectedObject.transform.parent = newImageTarget.transform;
                selectedObject.transform.position = newImageTarget.transform.GetChild(1).transform.position;
                selectedObject.transform.position = Vector3.zero;
                selectedObject.GetComponent<Transform>().localScale = newImageTarget.transform.GetChild(1).GetComponent<Renderer>().bounds.size;
                newImageTarget.transform.name = selectedObject.name + " Position";
                selectedObject.transform.localEulerAngles = Vector3.zero;

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
                /// <summary>
                /// Loads and adds all of the components to the instantiated object so that you can scale it, rotate it and change its animations.
                /// </summary>
                #region Singleton

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

    /// <summary>
    /// Error out if vuforia crashes.
    /// </summary>
    private void ErrorBack()
    {
        errorUI.SetActive(false);
    }
}