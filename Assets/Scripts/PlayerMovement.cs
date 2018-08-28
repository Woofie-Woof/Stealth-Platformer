using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool onGround;
	public bool doubleJump;
	public bool wallCling;
	public bool move;
	public int jumpStrength;
	public int movementSpeed;
	private Vector2 wallDir;
	private Vector2 tempDir;
	private Rigidbody2D rbd;
	// Use this for initialization
	void Start () {
		onGround = false;
		doubleJump = false;
		wallCling = false;
		move = true;
		rbd = GetComponent<Rigidbody2D>();
		jumpStrength = 175;
		movementSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("left") && move){
			transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
		}

		if(Input.GetKey("right") && move){
			transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
		}

		if(Input.GetKeyDown("space") && move){
			if(onGround || (!wallCling && doubleJump)){
				rbd.AddForce(Vector2.up * jumpStrength);
				doubleJump = onGround == false ? false : doubleJump;
			}
			// else if(wallCling){
			// 	rbd.AddForce(wallDir * jumpStrength);
			// }
		}

		if(Input.GetKey("space")){
			if(wallCling){
				move = false;
				rbd.velocity = Vector2.zero;
				rbd.angularVelocity = 0f;
				rbd.gravityScale = 0;
			}
		}

		if(Input.GetKeyUp("space")) {
			if(rbd.gravityScale == 0){
				rbd.gravityScale = 1;
				move = true;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Ground"){
			onGround = true;
			doubleJump = true;
			wallCling = false;
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.tag == "Wall" && !onGround){
			tempDir = other.GetContact(0).point - (new Vector2(transform.position.x, transform.position.y));
			tempDir = -tempDir.normalized;
			wallDir = new Vector2(tempDir.x, 1);
			wallCling = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Ground"){
			onGround = false;
		}
		else if(other.gameObject.tag == "Wall"){
			wallCling = false;
		}
	}
}
