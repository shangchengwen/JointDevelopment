using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KIT
{
    //因为使用StartCoroutine协程所以要继承MonoBehaviour 因而MOFLoadAssetBundle要挂载
    public class LoadAssetBundle : MonoBehaviour
    {

        public bool isEditor = false;

        public static LoadAssetBundle Instance;

        //以字典的形式保存加载好的模型，以便再次加载时不需要再次用www去加载，需要定时清空
        Dictionary<string, Object> objectList = new Dictionary<string, Object>();

        void Awake()
        {
            Instance = this;
        }

        //模型加载成功后的回调
        public delegate void OnLoadedDelegate(object objLoaded, object param);

        public void StartLoad(string assetBundlePath, string assetBundleName, OnLoadedDelegate loadedDelegate, object param)
        {
            //编辑器模式下，直接通过Profab右下角的AssetBundle名称加载模型，不需要对模型进行打包
#if UNITY_EDITOR
            if (isEditor)
            {
                string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundlePath, assetBundleName);
                if (assetPaths.Length == 0)
                {
                    Debug.LogError("There is no asset with name \"" + assetBundlePath + "\" in " + assetBundleName);
                }
                else
                {
                    Debug.Log(assetPaths[0]);
                }
                Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
                loadedDelegate(target, param);
            }
            else
            {
                //打包成程序后 模型必须打包后才能加载模型
                StartCoroutine(Load(assetBundlePath, assetBundleName, loadedDelegate, param));
            }
#else
        //打包成程序后 模型必须打包后才能加载模型
        StartCoroutine(Load(assetBundlePath, assetBundleName, loadedDelegate, param));
#endif
        }

        IEnumerator Load(string assetBundlePath, string assetBundleName, OnLoadedDelegate loadedDelegate, object param)
        {
            //如果模型字典中已经存在模型了，就可以直接加载
            if (objectList.ContainsKey(assetBundlePath + "/" + assetBundleName))
            {
                //返回(模型，参数)
                loadedDelegate(objectList[assetBundlePath + "/" + assetBundleName], param);
                yield break;
            }
            //在指定文件夹中加载模型，这个根据打包时指定的打包位置有关 Application.streamingAssetsPath当前文件中StreamingAssets文件夹
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/AssetBundle" + "/Windows/" + assetBundlePath);
            yield return www;
            AssetBundle bundle = www.assetBundle;
            //bundle中可能包含不只一个模型
            Object target = bundle.LoadAsset(assetBundleName);
            //在模型的字典中添加模型，方便下次加载
            objectList.Add(assetBundlePath + "/" + assetBundleName, target);

            //返回(模型，参数)
            loadedDelegate(target, param);
        }

        public void Clean()
        {
            objectList.Clear();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.LogError(objectList.Count);
            }
        }
    }
}

