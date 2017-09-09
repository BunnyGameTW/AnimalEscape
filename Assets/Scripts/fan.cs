using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour {
    public float fanSpeed=10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(new Vector3( fanSpeed*Time.deltaTime,0,0));

    }
}
