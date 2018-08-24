using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool onGround;
	// Use this for initialization
	void Start () {
		onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("left")){
			transform.Translate(Vector2.left * Time.deltaTime);
		}

		if(Input.GetKey("right")){
			transform.Translate(Vector2.right * Time.deltaTime);
		}

		if(Input.GetKeyDown("space")){
			if(onGround){
				transform.Translate(Vector2.up * Time.deltaTime * 10);
				onGround = false;
			}
		}
	}

	private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "Ground"){
			onGround = true;
		}
	}
}
