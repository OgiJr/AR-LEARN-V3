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
        public AssetBundle[] bundle;
        public string[] text;
    }

    internal WWW www;
    internal WWW www1;
    internal WWW getTextWWW;

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
        getTextWWW = new WWW(url);
        while (!getTextWWW.isDone);
        results = getTextWWW.text;
        p = JsonUtility.FromJson<Package>(results);
        p.id = id;
        p.bundle = new AssetBundle[p.models];
        p.text = new string[p.models];
        PlayerPrefs.SetString("ID", id);
    }

    /// <summary>
    /// Download the models and markdown files for a package
    /// </summary>
    /// <param name="p"> Package whose models to download </param>
    public void downloadModels()
    {
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "/assets"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/assets");
        }

        for (int i = 0; i < p.models; i++)
        {
            if (!System.IO.File.Exists(Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".unity3d"))
            {
                www = new WWW("https://arlearn.xyz/models/" + p.id + "_" + i + ".unity3d");
                while (!www.isDone);
                p.bundle[i] = www.assetBundle;
                System.IO.File.WriteAllBytes(Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".unity3d", www.bytes);
            }
            else
            {
                p.bundle[i] = AssetBundle.LoadFromFile(Application.persistentDataPath + "/assets/" + p.id + "_" + i + ".unity3d");
            }
            p.bundle[i].name = p.id + "_" + i;
            www1 = new WWW("https://arlearn.xyz/markdown/" + p.id + "_" + i + ".md");
            while (!www1.isDone);
            p.text[i] = www1.text;
            Debug.Log(p.text[0]);
        }
    }
}