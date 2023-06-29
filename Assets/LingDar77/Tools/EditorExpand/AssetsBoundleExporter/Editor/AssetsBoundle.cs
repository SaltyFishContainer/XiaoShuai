using UnityEditor;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
public class AssetsBoundle
{

    [MenuItem("AssetsBoundle/BuildAssetsBoundles")]
    static void BuildAssetsBoundles()
    {
        string dir = "AssetBundles";
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
#endif