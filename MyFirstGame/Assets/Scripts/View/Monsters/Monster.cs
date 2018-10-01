using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : Unit 
{
	protected int health;
	protected float speed;
	protected int damage;
	protected float velocity;
	protected float damageArea;
	protected float damageRate;

	protected MonsterState state;

	protected float triggerArea;

	protected Transform character;

	public float xMinPos;
	public float xMaxPos;
	protected Movement movement;

	protected float getDamagePower;
	[SerializeField]
	private Rigidbody2D rb;

	Coroutine damageCoroutine;


	#region Unity lifecycle
	void Awake()
	{
		Messenger.AddListener(GameEvent.CHARACTER_SEEMED, OnCharacterSeemed);
		Messenger.AddListener(GameEvent.CHARACTER_HIDED, OnCharacterHided);
	}

	void Start()
	{
		state = MonsterState.Find;
	}

	// ПАТТЕРН "ШАБЛОННЫЙ МЕТОД"
	void Update()
	{
		if (state == MonsterState.Find)
		{
			foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, triggerArea))
				if (collider.gameObject.tag == "character")
				{
					SetTriggered();
				}
		}
		else
		{
			if (state == MonsterState.Triggered)
			{
				foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, damageArea))
					if (collider.gameObject.tag == "character")
					{
						SetAgressive();
						break;
					}
			}
		}
		Move();
	}
	#endregion

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.CHARACTER_SEEMED, OnCharacterSeemed);
		Messenger.RemoveListener(GameEvent.CHARACTER_HIDED, OnCharacterHided);
	}

	public void OnHit(MessageParameters parameters)
	{
		health--;
		if (health < 1)
			Destroy(gameObject);
		Vector2 getDamageForce = new Vector2(0.1f, 1);
		getDamageForce.x *= parameters.direction;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
		}

		if (state == MonsterState.Calm || state == MonsterState.Find)
            SetTriggered();
	}

	public void OnCharacterHided()
	{
		if(damageCoroutine != null)
			StopCoroutine(damageCoroutine);
		SetCalm();
	}

	public void OnCharacterSeemed()
	{
		state = MonsterState.Find;
	}

	protected void Move()
	{
		if(state != MonsterState.GetHit)
			transform.position = Vector2.Lerp(transform.position, movement.Move(), speed * Time.deltaTime);
	}


	IEnumerator Damaging()
	{
		Attack();
		yield return new WaitForSeconds(damageRate);
		SetTriggered();
	}

	protected virtual void Attack()
	{
	}

	protected virtual void SetCalm()
	{
		state = MonsterState.Calm;
	}

	protected virtual void SetTriggered()
	{
		state = MonsterState.Triggered;
	}

	protected virtual void SetAgressive()
	{
		state = MonsterState.Attacked;
		damageCoroutine = StartCoroutine(Damaging());
	}

	protected void OnChangeFlipX(bool flipX)
	{
		FlipX = flipX;
	}
}