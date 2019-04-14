using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
/// <summary>
/// AlterAssetBundle类为修改批量修改AssetBundle的Name与Variant的编辑器窗口
/// </summary>
public class AlterAssetBundle : EditorWindow
{
    [MenuItem("BuildAssetBundle/SetAssetBundlePath")]
    static void AddWindow()
    {
        //创建窗口
        AlterAssetBundle window = (AlterAssetBundle)EditorWindow.GetWindow(typeof(AlterAssetBundle), false, "批量修改AssetBundle");
        window.Show();
    }

    //输入文字的内容
    private string _path = "Assets/Resources/";
    private string Variant = "";

    //通过点击获取文件夹
    private bool clickSelectFolder = false;

    void OnGUI()
    {
        GUIStyle text_style = new GUIStyle();
        text_style.fontSize = 15;
        text_style.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("点击项目中需要设置AssetBundle的文件夹", GUILayout.MinWidth(60));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("会截取文件夹名为\"AssetBundle\"后的文件夹名", GUILayout.MinWidth(60));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("文件夹路径", GUILayout.MinWidth(60));
        _path = EditorGUILayout.TextField(_path);
        _path = AssetDatabase.GetAssetPath(Selection.activeObject);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Variant:", GUILayout.MinWidth(120));
        Variant = EditorGUILayout.TextField(Variant.ToLower());
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("修改该文件夹下的AssetName及Variant"))
        {
            SetSettings();
        }

        if (GUILayout.Button("清除所有未被引用的AssetName及Variant"))
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        if (GUILayout.Button("清空所有AssetName及Variant"))
        {
            ClearAssetBundlesName();
        }
    }
    
    /// <summary>
    /// 此函数用来修改AssetBundleName与Variant
    /// </summary>
    void SetSettings()
    {
        if (Directory.Exists(_path))
        {
            DirectoryInfo direction = new DirectoryInfo(_path);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);


            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                AssetImporter ai = AssetImporter.GetAtPath(files[i].FullName.Substring(files[i].FullName.IndexOf("Assets")));

                string preSplitStr = files[i].DirectoryName+ Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(files[i].FullName);
                string splitStr = "AssetBundle";
                string[] sArray = Regex.Split(preSplitStr, splitStr, RegexOptions.IgnoreCase);
                if (sArray.Length < 2)
                {
                    Debug.Log("没有路径名称为AssetBundle，只设置AssetBundle文件下的文件");
                    return;
                }
                string endPath = sArray[sArray.Length - 1].Substring(1);

                    ai.SetAssetBundleNameAndVariant(endPath, Variant);
            }
            Debug.Log("设置成功");
            AssetDatabase.Refresh();
        }
    }

    /// <summary>
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
    /// 工程中只要设置了AssetBundleName的，都会进行打包
    /// </summary>
    static void ClearAssetBundlesName()
    {
        int length = AssetDatabase.GetAllAssetBundleNames().Length;
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
    }
     
    void OnInspectorUpdate()
    {
        this.Repaint();//窗口的重绘
    }
}