using System;
using UnityEngine.Networking;
using UnityEngine;

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
}

public class ServerDownloader
{
    /// <summary>
    /// This function returns a serializable class with info for an AR package
    /// </summary>
    /// <param name="id"> The unique id of the AR Package </param>
    /// <returns> The package class </returns>
    public static Package getInfo(string id)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://arlearn.xyz/getinfo.php?id=" + id);
        www.SendWebRequest();
        byte[] results = www.downloadHandler.data;

        Package p = JsonUtility.FromJson<Package>(System.Text.Encoding.UTF8.GetString(results));
        p.id = id;
        return p;
    }

    /// <summary>
    /// Download the models and markdown files for a package
    /// </summary>
    /// <param name="p"> Package whose models to download </param>
    public static void downloadModels(Package p)
    {
        for(int i = 0; i < p.models; i++)
        {
            UnityWebRequest www = UnityWebRequest.Get("https://arlearn.xyz/getmodel.php?id=" + p.id + "&n=" + i);

        }
    }
}
