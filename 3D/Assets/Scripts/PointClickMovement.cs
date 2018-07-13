using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
	private Vector3 _targetPos = Vector3.one;
	private float _curSpeed = 0f;
	public float deceleration = 20f;
	public float targetBuffer = 1.5f;

	[SerializeField]
	private Camera mainCamera;



	public float rotSpeed = 15f;

	public float moveSpeed = 6f;
	public float jumpSpeed = 15f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10f;
	public float minFall = -1.5f;

	public float pushForce = 3f;

	private float _vertSpeed;
	private ControllerColliderHit _contact;

	private CharacterController _charController;

	private Animator _animator;

	private Vector3 movement;

	// Use this for initialization
	void Start()
	{
		movement = Vector3.zero;
		_charController = GetComponent<CharacterController>();
		_vertSpeed = minFall;
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 movement = Vector3.zero;


		// ОПРЕДЕЛЕНИЕ ТОЧКИ ДВИЖЕНИЯ
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit mouseHit;
			if (Physics.Raycast(ray, out mouseHit))
			{
				GameObject hitObject = mouseHit.transform.gameObject;
				if (hitObject.layer == LayerMask.NameToLayer("Ground"))
				{
					_targetPos = mouseHit.point;
					_curSpeed = moveSpeed;
				}
			}
		}


		// ПОВОРОТ
		if (_targetPos != Vector3.one)
		{
			Vector3 adjustedPos = new Vector3(_targetPos.x, transform.position.y, _targetPos.z);
			Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);


			// ДВИЖЕНИЕ 
			movement = _curSpeed * Vector3.forward;
			movement = transform.TransformDirection(movement);

			if (Vector3.Distance(_targetPos, transform.position) < targetBuffer)
			{
				_curSpeed -= deceleration * Time.deltaTime;
				if (_curSpeed <= 0)
					_targetPos = Vector3.one;
			}
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
			//if (Input.GetButtonDown("Jump"))
			//	_vertSpeed = jumpSpeed;
			//else
			//{
				_vertSpeed = minFall;
				_animator.SetBool("Jumping", false);
			//}
		}
		else
		{
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity)
				_vertSpeed = terminalVelocity;

			if (_contact != null)
				_animator.SetBool("Jumping", true);


			// СТОИТ НА КРАЮ
			if (_charController.isGrounded)
			{
				if (Vector3.Dot(movement, _contact.normal) < 0)
				{ movement = _contact.normal * moveSpeed; }
				else
				{ movement += _contact.normal * moveSpeed; }
			}
		}

		movement.y = _vertSpeed;


		movement *= Time.deltaTime;
		Debug.Log(movement.x + ", " + movement.z);
		_charController.Move(movement);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_contact = hit;

		Rigidbody rb = hit.collider.attachedRigidbody;
		if (rb != null && !rb.isKinematic)
			rb.velocity = hit.moveDirection * pushForce;
	}
}
