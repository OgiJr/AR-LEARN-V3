using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class TheGiver : MonoBehaviour
{
    private void Update()
    {
        PostProcessingBehaviour pp = this.gameObject.GetComponent<PostProcessingBehaviour>();
        Camera.main.gameObject.AddComponent<PostProcessingBehaviour>();
        Camera.main.gameObject.GetComponent<PostProcessingBehaviour>().profile = pp.profile;
    }
}
