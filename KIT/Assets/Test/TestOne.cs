using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary1;

public class TestOne : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Class1 c = new Class1();
        Debug.LogError(c.Add(3,4));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
