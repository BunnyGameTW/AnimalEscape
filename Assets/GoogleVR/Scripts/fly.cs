using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour {
	// Use this for initialization
	public float speed =0.05f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 flyway = Camera.main.transform.forward;//取得當前方向
		transform.position += flyway*speed;
	}
}
