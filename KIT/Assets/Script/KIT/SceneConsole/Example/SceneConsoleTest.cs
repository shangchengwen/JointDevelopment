using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KIT;

public class SceneConsoleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Debug debug,这是一个测试");
        Debug.LogError("Debug error,这是一个测试");
        Debug.LogWarning("Debug warning,这是一个测试");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
