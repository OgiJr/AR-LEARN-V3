using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissWarning : MonoBehaviour
{
    public void Dismiss()
    {
        Destroy(this.gameObject);
        PlayerPrefs.SetInt("Dismissed", 1);
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("Dismissed") == 1)
        {
            Dismiss();
        }
    }
}
