using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster 
{
	void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 1;
		damageRate = 1f;

		triggerArea = 3f;

		sprite = GetComponentInChildren<SpriteRenderer>();

		movement = new IntelligenceMovement(sprite, transform);
		triggerMovement = new AgressiveMovement(sprite, transform, character);
		attackMovement = new StayInPlaceMovement(sprite, transform, character);
		attackMethod = (damage, velocity) => { Managers.Player.ChangeHealth(damage, sprite); };
	}
}
