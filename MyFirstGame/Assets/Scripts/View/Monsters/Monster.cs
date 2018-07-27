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

	protected float triggerArea;
	private bool trigger;

	[SerializeField]
	protected Transform character;
	protected SpriteRenderer sprite;

	public float xMinPos;
	public float xMaxPos;
	protected IMovement movement;
	protected IMovement triggerMovement;
	protected IMovement attackMovement;
	protected Action<int, float> attackMethod;

	protected float getDamagePower;
	protected Rigidbody2D rb;
	bool getHit;

	void Awake()
	{
		health = 1;
		speed = 1f;
		damage = -1;
		velocity = 1f;
		damageArea = 1;
		damageRate = 1f;
		damaging = false;

		triggerArea = 3f;
		trigger = false;

		sprite = GetComponentInChildren<SpriteRenderer>();

		xMinPos = 0f;
		xMaxPos = 0f;
		movement = new StayInPlaceMovement(sprite, transform, character);
		triggerMovement = movement;
		attackMovement = movement;
		attackMethod = (damage, velocity) => { };

		getDamagePower = 1f;
		getHit = false;
	}

	public void OnHit(MessageParameters parameters)
	{
		StartCoroutine(GetHit());
		health--;
		if (health < 1)
			Destroy(gameObject);
		Vector2 getDamageForce = new Vector2(0.1f, 1);
		int getDamageDiretion = parameters.Sprite.flipX ? -1 : 1;
		getDamageForce.x *= getDamageDiretion;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
		}
	}

	IEnumerator GetHit()
	{
		getHit = true;
		yield return new WaitForSeconds(1);
		getHit = false;
	}

	protected void Move()
	{
		if(!getHit)
			transform.position = Vector2.Lerp(transform.position, movement.Move(), speed* Time.deltaTime);
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
		Move();	}
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