using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool onGround;
	public bool doubleJump;
	public bool wallJump;
	public int jumpStrength;
	private Vector2 wallDir;
	private Vector2 tempDir;
	private Rigidbody2D rbd;
	// Use this for initialization
	void Start () {
		onGround = false;
		doubleJump = false;
		wallJump = false;
		rbd = GetComponent<Rigidbody2D>();
		jumpStrength = 175;
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
			if(onGround || (!wallJump && doubleJump)){
				rbd.AddForce(Vector2.up * jumpStrength);
				doubleJump = onGround == false ? false : doubleJump;
			}
			else if(wallJump){
				rbd.AddForce(wallDir * jumpStrength/2);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Ground"){
			onGround = true;
			doubleJump = true;
			wallJump = false;
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.tag == "Wall" && !onGround){
			tempDir = other.GetContact(0).point - (new Vector2(transform.position.x, transform.position.y));
			tempDir = -tempDir.normalized;
			wallDir = new Vector2(tempDir.x, 1);
			wallJump = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Ground"){
			onGround = false;
		}
		else if(other.gameObject.tag == "Wall"){
			wallJump = false;
		}
	}
}
