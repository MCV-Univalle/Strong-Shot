using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTarget : MonoBehaviour {

	public float spinSpeed = 180.0f;
	public float moveSpeed = 0.1f;


	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.Rotate (Vector3.up*spinSpeed*Time.deltaTime);	
		gameObject.transform.Translate(Vector3.left*Mathf.Sin(Time.timeSinceLevelLoad)*moveSpeed);
	}
}
