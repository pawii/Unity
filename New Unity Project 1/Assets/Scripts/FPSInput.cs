using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {

	public float baseSpeed = 6f;
	public float gravity = -9f;

	private CharacterController characterController;
	private float deltaHor;
	private float deltaVer;
	private float speed;

	// Use this for initialization
	void Start () {
		speed = baseSpeed;
		characterController = GetComponent<CharacterController> ();
	}

	void Awake()
	{
		Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnDestroy()
	{
		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnSpeedChanged(float settingSpeed)
	{
		speed = baseSpeed * settingSpeed;
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
