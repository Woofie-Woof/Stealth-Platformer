using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillate : MonoBehaviour {

	public Vector3 from;
	public Vector3 to;
	public float speed;
	// Use this for initialization
	void Start () {
		from = new Vector3(53f, -30f, 0f);
		to = new Vector3(53f, 30f, 0f);
		speed = 0.3f;
	}
 
 void Update() {
     float t = (Mathf.Sin (Time.time * speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
     transform.eulerAngles = Vector3.Lerp(from, to, t);
 }
}
