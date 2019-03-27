using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class AssetBuilder : Editor
{
    public static void ExportBundle()
    {
        String[] arguments = Environment.GetCommandLineArgs();
        string id = arguments[7];
        int num = Int32.Parse(arguments[8]);
        Object[] objs = new Object[num];
        Object[] texts = new Object[num];
        Object[] final = new Object[num * 2];

        int j = 0;
        for(int i = 0; i < num; i++)
        {
            objs[i] = Resources.Load<Object>("markdown/" + id + "_" + i);
            texts[i] = Resources.Load<Object>("models/" + id + "_" + i);
            final[j++] = objs[i];
            final[j++] = texts[i];
        }

        string bundlePath = "Assets/AssetBundle/" + id + ".unity3d";
        BuildPipeline.BuildAssetBundles(
            final[0],
            final,
            bundlePath,
            BuildTarget.Android);
    }
}
