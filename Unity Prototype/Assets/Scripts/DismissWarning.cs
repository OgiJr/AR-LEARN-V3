using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissWarning : MonoBehaviour
{
    public void Dismiss()
    {
        Destroy(this.gameObject);
    }
}
