using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTarget : MonoBehaviour {

	public float spinSpeed = 180.0f;
	public float moveSpeed = 0.1f;
	private float acu = 0.0f;
	public bool dir = false;

	public float deadPosition = 0;


	
	// Update is called once per frame


	void Update () {
		if (dir) { 
			gameObject.transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		} else {
			gameObject.transform.Translate (Vector3.left * Time.deltaTime * moveSpeed);
		}

		if(gameObject.transform.position.x >= deadPosition){
			gameObject.SetActive (false);
		}
	}
}
