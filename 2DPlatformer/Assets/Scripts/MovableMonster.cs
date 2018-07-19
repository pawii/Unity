using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovableMonster : Monster 
{
	[SerializeField]
	private float speed = 2f;

	private Vector3 direction;

	private SpriteRenderer sprite;

	void Awake()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		Unit unit = collider.GetComponent<Unit>();

		if (unit && unit is Character)
		{			if (Mathf.Abs(transform.position.x - unit.transform.position.x) < 0.5f) ReceiveDamage();			else unit.ReceiveDamage();
		}
	}

	void Start()
	{
		direction = transform.right;
	}

	void Update()
	{
		Move();
	}

	private void Move()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + 
		                                                    transform.right * direction.x * 0.5f, 0.1f);
		if (colliders.Length > 0)// && colliders.All(x => !x.GetComponent<Character>()))
			direction *= -1;

		transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
	}
}
