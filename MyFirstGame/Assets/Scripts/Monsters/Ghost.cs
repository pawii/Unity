using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster 
{
	void Awake()
	{
		speed = 3f;
		health = 3;
		sprite = GetComponentInChildren<SpriteRenderer>();
		movement = new IntelligenceMovement(-1, sprite, transform, null); // ПАТТЕРН "СТРАТЕГИЯ"
	}

	void Update()
	{
		// ПАТТЕРН "СТРАТЕГИЯ"
		if (!trigger)
		{
			foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, triggerArea))
				if (collider.gameObject.tag == "character")
				{
					trigger = true;
					movement = new AgressiveMovement(movement.Direction, sprite, transform, collider.gameObject.transform);
				}
		}
		Move();
		// ПАТТЕРН "СТРАТЕГИЯ"
	}
}
