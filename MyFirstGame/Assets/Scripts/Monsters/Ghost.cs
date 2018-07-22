using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster 
{
	void Start()
	{
		speed = 3f;
		health = 3;
		damage = -1;
		damageArea = 2f;
		damageRate = 2f;
		triggerArea = 5f;
		sprite = GetComponentInChildren<SpriteRenderer>();
		movement = new IntelligenceMovement(-1, sprite, transform, null);          // ПАТТЕРН "СТРАТЕГИЯ"
		triggerMovement = new AgressiveMovement(-1, sprite, transform, character); // ПАТТЕРН "СТРАТЕГИЯ"
		attackMovement = new GhostAttackMovement(-1, null, transform, null);
		attackMethod = (damage) => { Managers.Player.ChangeHealth(damage, sprite); };
	}
}
