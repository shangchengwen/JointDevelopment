using KIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundleTest : MonoBehaviour {

    string assetBundlePath = "game/cube";
    string assetBundleName = "cube";
    Dictionary<string, int> numlist = new Dictionary<string, int>();
    Hashtable param = new Hashtable();
    int num = 1;
    private void Start()
    {
        param.Add(1, 1);
        param["name"] = "Joshua";
        param["age"] = 18;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadAssetBundle.Instance.StartLoad(assetBundlePath, assetBundleName, LoadPrefab, param);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadAssetBundle.Instance.Clean();
        }
    }
    public void LoadPrefab(object objLoaded, object param)
    {
        GameObject animal = GameObject.Instantiate(objLoaded as GameObject);
        animal.transform.position = new Vector3(0, 0, num);
        num++;
        if (param != null)
        {
            Hashtable temp = param as Hashtable;
            Debug.LogError(temp[1]);
            Debug.LogError(temp["name"]);
            Debug.LogError(temp["age"]);
        }
    }
}
