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

	private Animator _animator;

	private Vector3 movement;

	// Use this for initialization
	void Start () {
		movement = Vector3.zero;
		_charController = GetComponent<CharacterController>();
		_vertSpeed = minFall;
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 movement = Vector3.zero;


		// ДВИЖЕНИЕ ПО ГОРИЗОНТАЛИ
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
		}

		_animator.SetFloat("Speed", movement.sqrMagnitude);



		// РАСПОЗНОВАНИЕ ПОВЕРХНОСТИ
		bool hitGround = false;
		RaycastHit hit;
		if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) 
		{
			float check = // РАССТОЯНИЕ ЧУТЬ ВЫХОДИТ ЗА ГРАНИЦЫ КАПСУЛЫ
				(_charController.height + _charController.radius) / 1.9f;
			hitGround = hit.distance <= check;
		}



		// СОСТОЯНИЯ: 
		// 1) ПЕРСОНАЖ ПРЫГАЕТ: _vertSpeed > 0 
		// 2) ПЕРСОНАЖ СТОИТ НА КРАЮ: 
		//       hitGround == false && _charController.isGrounded == true
		// 3) ПЕРСОНАЖ СТОИТ НА ПОВЕРХНОСТИ: hitGround == true;




		// ДВИЖЕНИЕ ПО ВЕРТИКАЛИ

		// СТОИТ НА ПОВЕРХНОСТИ ИЛИ ПРЫГАЕТ
		if (hitGround)
		{
			if (Input.GetButtonDown("Jump"))
				_vertSpeed = jumpSpeed;
			else
			{ 
				_vertSpeed = minFall;
				_animator.SetBool("Jumping", false);
			}
		}
		else 
		{
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed<terminalVelocity)
				_vertSpeed = terminalVelocity;

			if (_contact != null)
				_animator.SetBool("Jumping", true);


			// СТОИТ НА КРАЮ
			if (_charController.isGrounded)
			{
				if (Vector3.Dot(movement, _contact.normal) < 0)
				{ movement = _contact.normal * moveSpeed;}
				else
				{ movement += _contact.normal * moveSpeed; }
			}
		}

		movement.y = _vertSpeed;


		movement *= Time.deltaTime;
		_charController.Move(movement);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_contact = hit;
	}
}
