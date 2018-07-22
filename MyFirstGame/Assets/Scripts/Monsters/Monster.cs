using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour 
{
	protected int health;
	protected float speed;
	protected IMovement movement;
	protected SpriteRenderer sprite;
	protected bool trigger = false;
	protected float triggerArea = 5f;

	void Awake()
	{
		health = 0;
		speed = 1f;
		//sprite = GetComponentInChildren<SpriteRenderer>();
		movement = new StayInPlaceMovement(-1, sprite, transform, null); // ПАТТЕРН "СТРАТЕГИЯ"
	}

	void OnHit()
	{
		health--;
		Debug.Log(health);
		if (health < 1)
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		CharacterMovement character = collision.gameObject.GetComponent<CharacterMovement>();

		if (character)                 // collision.gameObject.tag == "character"
		{
			character.RecieveDamage(); // Managers.Player.RecieveDamage();
		}
	}

	// ПАТТЕРН "СТРАТЕГИЯ"
	protected void Move()
	{
		transform.position = Vector2.MoveTowards(transform.position, movement.Move(), speed* Time.deltaTime);
	}
	// ПАТТЕРН "СТРАТЕГИЯ"
}
