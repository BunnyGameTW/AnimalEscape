using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class HumanMove : MonoBehaviour {
	public Vector2 Way2=new Vector2(0,0);
	public Vector3 Way3=new Vector3(0,0,0);
	public Vector3 Move=new Vector3(0,0,0);
	public float x, y, z;


	public float Speed=0.7f;

	// Use this for initialization
	void Start () {
		//Way3 = Camera.main.transform.forward;//取得當前方向向量

	}
	
	// Update is called once per frame
	void Update () {
		/////////////////////////////////////////////////////////////////////////////////////
		float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float vertical = CrossPlatformInputManager.GetAxis("Vertical");

		Move.x = CrossPlatformInputManager.GetAxis ("Horizontal");
		Move.z = CrossPlatformInputManager.GetAxis ("Vertical");
		if (Move.x> 0 || Move.y > 0) {
			transform.position +=Move*Speed;
		}
			

	}
}
