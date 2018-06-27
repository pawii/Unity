using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {

	public float speed = 6f;
	public float gravity = -9f;

	private CharacterController characterController;
	private float deltaHor;
	private float deltaVer;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		deltaHor = Input.GetAxis ("Horizontal") * speed;
		deltaHor *= Time.deltaTime;
		deltaVer = Input.GetAxis ("Vertical") * speed;
		deltaVer *= Time.deltaTime;
		Vector3 movement = new Vector3 (deltaHor, gravity, deltaVer);
		movement =  Vector3.ClampMagnitude (movement, speed);
		movement = transform.TransformDirection (movement);
		characterController.Move (movement);
	}
}
