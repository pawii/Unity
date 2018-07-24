using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour 
{
	protected int health;
	protected float speed;
	protected int damage;
	protected float velocity;
	protected float damageArea;
	protected float damageRate;
	private bool damaging;

	protected IMovement movement;
	protected IMovement triggerMovement;
	protected IMovement attackMovement;
	protected Action<int, float> attackMethod;

	protected SpriteRenderer sprite;
	private bool trigger = false;
	protected float triggerArea;
	[SerializeField]
	protected Transform character;

	void Awake()
	{
		velocity = 0f;
		health = 0;
		speed = 1f;
		damage = 0;
		damageArea = 0;
		damageRate = 0f;
		damaging = false;
		triggerArea = 0f;
		sprite = GetComponentInChildren<SpriteRenderer>();
		movement = new StayInPlaceMovement(-1, sprite, transform, null); // ПАТТЕРН "СТРАТЕГИЯ
		triggerMovement = movement;
		attackMovement = movement;
		attackMethod = (damage, velocity) => { };
	}

	void OnHit()
	{
		health--;
		Debug.Log(health);
		if (health < 1)
			Destroy(gameObject);
	}

	/*protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		CharacterMovement character = collision.gameObject.GetComponent<CharacterMovement>();

		if (character)                
		{
			character.RecieveDamage(sprite); 
		}
	}*/

	protected void Move()
	{
		transform.position = Vector2.MoveTowards(transform.position, movement.Move(), speed* Time.deltaTime);
	}


	// ПАТТЕРН "ШАБЛОННЫЙ МЕТОД"
	void Update()
	{
		if (!trigger)
		{
			foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, triggerArea))
				if (collider.gameObject.tag == "character")
				{
					trigger = true;
					movement = triggerMovement;
				}
		}
		else
		{
			if (!damaging)
			{
				foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, damageArea))
					if (collider.gameObject.tag == "character")
					{
						StartCoroutine(Damaging());
						movement = attackMovement;
					}
			}
		}
		Move();
		//Debug.Log(movement.ToString());	}
	IEnumerator Damaging()
	{
		damaging = true;
		attackMethod.Invoke(damage, velocity);
		yield return new WaitForSeconds(damageRate);
		damaging = false;
		movement = triggerMovement;
	}
	// ПАТТЕРН "ШАБЛОННЫЙ МЕТОД"

}