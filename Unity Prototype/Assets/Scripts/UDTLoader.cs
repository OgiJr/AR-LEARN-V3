using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads downloader AR models at arlearn.xyz
/// </summary>
public class UDTLoader : MonoBehaviour
{
    public GameObject current;
    public GameObject errorUI;
    private bool loaded = false;

    public GameObject load(string name)
    {
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log(Application.persistentDataPath + "/assets/" + name + ".unity3d");
        Debug.Log(System.IO.File.Exists(Application.persistentDataPath + "/assets/" + name + ".unity3d"));
        if (!System.IO.File.Exists(Application.persistentDataPath + "/assets/" + name + ".unity3d"))
        {
            if (loaded == false)
            {
                errorUI.SetActive(true);
                Invoke("RemoveUI", 2);
                loaded = true;
            }
        }
        else
        {
            AssetBundle ab = AssetBundle.LoadFromFile(Application.persistentDataPath + "/assets/" + name + ".unity3d");
            current = (GameObject)ab.mainAsset;
        }
        return current;
    }

    private void RemoveUI()
    {
        errorUI.SetActive(false);
    }
}
