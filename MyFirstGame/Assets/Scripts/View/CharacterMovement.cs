﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour 
{
	public float speed = 5f;
	public float jumpPower = 5f;
	[SerializeField]
	int getDamagePower = 15;

	private Rigidbody2D rb;
	public static bool Lock { private get; set; }

	private bool isGrounded = true;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		Lock = false;
	}

	void Update()
	{
		if (!Lock)
		{
			SetGrounded();
			if (Input.GetButton("Horizontal")) Run();
			if (Input.GetButtonDown("Jump") && isGrounded) Jump();
		}
	}

	void SetGrounded()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
		isGrounded = colliders.Length > 1;
	}

	void Run()
	{
		Vector2 direction = transform.right * Input.GetAxis("Horizontal");
		transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, 
		                                         speed * Time.deltaTime);
	}

	void Jump()
	{
		Vector2 force = transform.up * jumpPower;
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void OnHit(MessageParameters parameters)
	{
		GameController.ChangeHealth(parameters.Damage);

		Vector2 getDamageForce = new Vector2(0.1f, 1);
		int getDamageDiretion = parameters.Sprite.flipX ? 1 : -1;
		getDamageForce.x *= getDamageDiretion;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
		}
	}
}
