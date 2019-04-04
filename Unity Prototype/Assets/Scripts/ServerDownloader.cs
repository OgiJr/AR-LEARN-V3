using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

/// <summary>
/// Download AR Packages from the server at arlearn.xyz
/// </summary>
public class ServerDownloader : MonoBehaviour
{
    /// <summary>
    /// AR Package info class
    /// </summary>
    [Serializable]
    public class Package
    {
        public string id;
        public string name;
        public string description;
        public int models;
        public bool[] arePrefabs;
        public AssetBundle bundle;
        public string[] text;
    }

    public Package p;

    /// <summary>
    /// This function returns a serializable class with info for an AR package
    /// </summary>
    /// <param name="id"> The unique id of the AR Package </param>
    /// <returns> The package class </returns>
    public void getInfo(string id)
    {
        string results;
        string url = "https://arlearn.xyz/getinfo.php?id=" + id;
        WWW getTextWWW = new WWW(url);
        while (!getTextWWW.isDone) ;
        results = getTextWWW.text;
        p = JsonUtility.FromJson<Package>(results);
        p.id = id;
        p.bundle = null;
        p.text = new string[p.models];
    }

    /// <summary>
    /// Download the models and markdown files for a package
    /// </summary>
    /// <param name="p"> Package whose models to download </param>
    public IEnumerator downloadModels()
    {
        for (int i = 0; i < p.models; i++)
        {
            if (p.arePrefabs[i])
            {
                if (!System.IO.Directory.Exists("file://" + Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".unity3d"))
                {
                    UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://arlearn.xyz/models/" + p.id + "_" + i + ".unity3d");
                    yield return www.SendWebRequest();
                    p.bundle = DownloadHandlerAssetBundle.GetContent(www);
                    System.IO.File.WriteAllBytes("file://" + Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".unity3d", www.downloadHandler.data);
                } else
                {
                    AssetBundleCreateRequest bundle = AssetBundle.LoadFromFileAsync("file://" + Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".unity3d");
                    yield return bundle;
                    p.bundle = bundle.assetBundle;
                }

                Debug.Log(p.bundle.GetAllAssetNames());
            }
            else
            {
                if(!System.IO.Directory.Exists("file://" + Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".fbx"))
                {

                } else
                {

                }
            }
        }
    }

}
