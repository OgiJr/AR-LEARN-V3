using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class AssetBuilder : Editor
{
    public static void ExportBundle()
    {
        String[] arguments = Environment.GetCommandLineArgs();
        int arlen = arguments.Length;
        string id = arguments[arlen - 2];
        int num = Int32.Parse(arguments[arlen - 1]);
        UnityEngine.Object[] objs = new UnityEngine.Object[num];
        UnityEngine.Object[] texts = new UnityEngine.Object[num];
        UnityEngine.Object[] final = new UnityEngine.Object[num * 2];

        int j = 0;
        for(int i = 0; i < num; i++)
        {
            objs[i] = Resources.Load<UnityEngine.Object>("markdown/" + id + "_" + i);
            texts[i] = Resources.Load<UnityEngine.Object>("models/" + id + "_" + i);
            final[j++] = objs[i];
            final[j++] = texts[i];
        }

        string bundlePath = "Assets/AssetBundle/" + id + ".unity3d";
        BuildPipeline.BuildAssetBundle(
            final[0],
            final,
            bundlePath,
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
            BuildTarget.Android);
    }
}
