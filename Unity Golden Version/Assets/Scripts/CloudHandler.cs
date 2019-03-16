using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.PostProcessing;

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

    private bool changeable = true;
    private bool errorShown = false;
    public RuntimeAnimatorController exitAnimator;
    public UIEffects uiEffects;

    private GameObject follow;
    private bool instantiated = false;
    private bool detected = false;
    GameObject newImageTarget = null;

    void Start()
    {
        // register this event handler at the cloud reco behaviour
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
            selectedObject.transform.position = follow.transform.GetChild(0).transform.position;

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
                if (selectedObject.transform.GetChild(0).GetComponentInChildren<Renderer>() == true)
                {
                    foreach (Renderer renderer in selectedObject.transform.GetChild(0).GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = false;
                    }
                }
                if (selectedObject.GetComponentInChildren<Canvas>() != null)
                {
                    foreach (Canvas canvas in selectedObject.GetComponentsInChildren<Canvas>())
                    {
                        canvas.enabled = false;
                    }
                }

                if(selectedObject.GetComponentInChildren<AudioSource>() != null)
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
                if (selectedObject.transform.GetChild(0).GetComponentInChildren<Renderer>() == true)
                {
                    foreach (Renderer renderer in selectedObject.transform.GetChild(0).GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = true;
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

        if(Camera.main.gameObject.GetComponent<PostProcessingBehaviour>() != null)
        {
            Camera.main.gameObject.GetComponent<PostProcessingBehaviour>().enabled = false;
        }
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

        if (Resources.Load("Objects/" + targetSearchResult.TargetName) != null)
        {
            GameObject augmentation = null;

            selectedObject = Instantiate(Resources.Load("Objects/" + targetSearchResult.TargetName) as GameObject);

            newImageTarget.transform.name = selectedObject.name + " Position";

            selectedObject.AddComponent<AdjustControls>();

            #region Singleton
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

    private void ErrorBack()
    {
        errorUI.SetActive(false);
    }
}