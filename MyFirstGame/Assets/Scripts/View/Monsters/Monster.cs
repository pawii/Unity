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

	private MonsterState state;

	protected float triggerArea;

	protected Transform character;
	protected SpriteRenderer sprite;

	public float xMinPos;
	public float xMaxPos;
	private IMovement movement;
	protected IMovement calmMovement;
	protected IMovement triggerMovement;
	protected IMovement attackMovement;
	protected Action<int, float> attackMethod;

	protected float getDamagePower;
	protected Rigidbody2D rb;

	Coroutine damageCoroutine;

	void Start()
	{
		state = MonsterState.Find;

		movement = calmMovement;

		HidePlace[] hidePlaces = FindObjectsOfType(typeof(HidePlace)) as HidePlace[];
		foreach (HidePlace hidePlace in hidePlaces)
			hidePlace.Attach(this);
	}

	public void OnHit(MessageParameters parameters)
	{
		Debug.Log("HIT");
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

	public void OnCharacterHided()
	{
		if(damageCoroutine != null)
			StopCoroutine(damageCoroutine);
		state = MonsterState.Calm;
		movement = calmMovement;
	}

	public void OnCharacterSeemed()
	{
		state = MonsterState.Find;
	}

	IEnumerator GetHit()
	{
		MonsterState tmp = state;
		state = MonsterState.GetHit;
		yield return new WaitForSeconds(1);
		state = tmp;
	}

	protected void Move()
	{
		if(state != MonsterState.GetHit)
			transform.position = Vector2.Lerp(transform.position, movement.Move(), speed* Time.deltaTime);
	}


	// ПАТТЕРН "ШАБЛОННЫЙ МЕТОД"
	void Update()
	{
		if (state == MonsterState.Find)
		{
			foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, triggerArea))
				if (collider.gameObject.tag == "character")
				{
					state = MonsterState.Triggered;
					movement = triggerMovement;
				}
		}
		else
		{
			if (state == MonsterState.Triggered)
			{
				foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, damageArea))
					if (collider.gameObject.tag == "character")
					{
						damageCoroutine = StartCoroutine(Damaging());
						movement = attackMovement;
					}
			}
		}
		Move();	}
	IEnumerator Damaging()
	{
		state = MonsterState.Attacked;
		attackMethod.Invoke(damage, velocity);
		yield return new WaitForSeconds(damageRate);
		state = MonsterState.Triggered;
		movement = triggerMovement;
	}
	// ПАТТЕРН "ШАБЛОННЫЙ МЕТОД"
}