using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSun : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("ChangeParticleSystem", 10);   
    }

    void ChangeParticleSystem()
    {
        this.gameObject.GetComponent<ParticleSystem>().emissionRate = 0.1f;
        this.gameObject.GetComponent<ParticleSystem>().playbackSpeed = 0.1f;
    }
}
