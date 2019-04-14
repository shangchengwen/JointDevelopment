using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

//1.本代码必须放在Editor文件夹下
//2.打包的Prefab为 右下角添加了AssetBundle路径的Prefab
//3.打包程序的时，要先打包AssetBundle，否则AssetBundle不会添加到程序中

/// <summary>
/// 打包 编辑器
/// </summary>
public class MOFBuildAssetBundleEditor : EditorWindow
{

    public static string GetOutPutPath()
    {
        //打包位置为本项目中StreamingAssets文件夹中 /AssetBundle/Windows 下
        string opp = Application.streamingAssetsPath + "/AssetBundle" + "/Windows";
        if (!Directory.Exists(opp))
        {
            Directory.CreateDirectory(opp);
        }
        return opp;
    }

    [MenuItem("BuildAssetBundle/BuildToWindows")]
    public static void BuildAssetBundle()
    {
        string outPutPath = GetOutPutPath();
        BuildPipeline.BuildAssetBundles(outPutPath, 0, EditorUserBuildSettings.activeBuildTarget);
    }
}