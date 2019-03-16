using UnityEngine.UI;
using UnityEngine;

public class BackgroundManagerSearchEngine : MonoBehaviour
{
    public void Selected()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(5).gameObject.SetActive(true);
    }
}
