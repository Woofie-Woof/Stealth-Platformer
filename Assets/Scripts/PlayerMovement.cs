using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool onGround;
	public bool doubleJump;
	public bool canWallCling;
	public bool clinged;
	public bool move;
	public float jumpStrength;
	public float movementSpeed;
	private float defaultSpeed;
	private Vector2 wallDir;
	private Vector2 tempDir;
	private Rigidbody2D rbd;
	public float wallClingWait;
	// Use this for initialization
	void Start () {
		onGround = false;
		doubleJump = false;
		canWallCling = false;
		clinged = false;
		move = true;
		rbd = GetComponent<Rigidbody2D>();
		jumpStrength = 210;
		defaultSpeed = 4;
		movementSpeed = 4;
		wallClingWait = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("left") && move){
			transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
		}

		if(Input.GetKey("right") && move){
			transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
		}

		if(Input.GetKey("left shift") && onGround){
			movementSpeed *= movementSpeed == defaultSpeed ? 2f : 1f;
		}
		else if(onGround || clinged){
			movementSpeed /= movementSpeed > defaultSpeed ? 2f : 1f;
		}

		if(Input.GetKeyDown("space") && move){
			if(onGround || (!canWallCling && doubleJump)){
				wallClingWait = onGround ? 0.2f : 0.0f;
				rbd.AddForce(Vector2.up * jumpStrength);
				doubleJump = onGround == false ? false : doubleJump;
			}
		}

		if(Input.GetKey("space")){
			if(canWallCling && wallClingWait <= 0){
				move = false;
				rbd.velocity = Vector2.zero;
				rbd.angularVelocity = 0f;
				rbd.gravityScale = 0;
				clinged = true;
			}
		}

		if(Input.GetKeyUp("space")) {
			if(clinged){
				rbd.gravityScale = 1;
				move = true;
				clinged = false;
				if(Input.GetKey("up")){
					rbd.AddForce(wallDir * jumpStrength);
				}
			}
		}

		if(wallClingWait > 0){
			wallClingWait -= Time.deltaTime;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Ground"){
			onGround = true;
			doubleJump = true;
			canWallCling = false;
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.tag == "Wall" && !onGround){
			tempDir = other.GetContact(0).point - (new Vector2(transform.position.x, transform.position.y));
			tempDir = -tempDir.normalized;
			wallDir = new Vector2(tempDir.x, 1);
			canWallCling = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Ground"){
			onGround = false;
		}
		else if(other.gameObject.tag == "Wall"){
			canWallCling = false;
		}
	}
}
