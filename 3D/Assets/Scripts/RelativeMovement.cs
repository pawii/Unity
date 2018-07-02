using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {

	[SerializeField]
	private Transform target;
	public float rotSpeed = 15f;

	public float moveSpeed = 6f;
	public float jumpSpeed = 15f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10f;
	public float minFall = -1.5f;

	private float _vertSpeed;
	private ControllerColliderHit _contact;

	private CharacterController _charController;

	private Vector3 movement;

	// Use this for initialization
	void Start () {
		movement = Vector3.zero;
		_charController = GetComponent<CharacterController>();
		_vertSpeed = minFall;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 movement = Vector3.zero;


		if (_charController.isGrounded)
		{
			if (Input.GetButtonDown("Jump"))
				_vertSpeed = jumpSpeed;
			else
				_vertSpeed = minFall;
		}
		else 
		{
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity)
				_vertSpeed = terminalVelocity;
		}


		float horInput = Input.GetAxis("Horizontal");
		float verInput = Input.GetAxis("Vertical");
		if (horInput != 0f || verInput != 0f)
		{
			movement.x = horInput * moveSpeed;
			movement.z = verInput * moveSpeed;
			movement = Vector3.ClampMagnitude(movement, moveSpeed);

			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
			movement = target.TransformDirection(movement);
			target.rotation = tmp;

			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);

			movement.y = _vertSpeed;
			movement *= Time.deltaTime;
			_charController.Move(movement);
		}
	}

	void OnControllerColladerHit(ControllerColliderHit hit)
	{
		_contact = hit;
	}
}
