using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceElement : MonoBehaviour
{
    public BounceApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<BounceApplication>();
        }
    }
	
}
