using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float speed = 6f;
	public float gravity = -9f;

	private CharacterController character;

	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3 (Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
		movement *= speed;
		movement *= Time.deltaTime;
		movement = Vector3.ClampMagnitude (movement, speed);
		movement = transform.TransformDirection (movement);
		character.Move (movement);
	}
}
