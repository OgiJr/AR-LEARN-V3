using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads downloader AR models at arlearn.xyz
/// </summary>
public class UDTLoader : MonoBehaviour
{
    public GameObject current;
    private GameObject errorUI;

    public GameObject load(string name)
    {
        errorUI = GameObject.Find("ErrorUI");
        if(!System.IO.File.Exists(Application.persistentDataPath + "/assets/" + name + ".unity3d")){
            errorUI.SetActive(true);
        }
        AssetBundle ab = AssetBundle.LoadFromFile(Application.persistentDataPath + "/assets/" + name + ".unity3d");
        current = Instantiate(ab.mainAsset as GameObject);
        return current;
    }
}
