using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster 
{
	void Awake()
	{
		speed = 3f;
		health = 3;
		damage = -1;
		damageArea = 1f;
		damageRate = 2f;
		triggerArea = 5f;
		sprite = GetComponentInChildren<SpriteRenderer>();
		movement = new IntelligenceMovement(-1, sprite, transform, null, null);
		triggerMovement = new AgressiveMovement(-1, sprite, transform, character, null);
		attackMovement = new GhostAttackMovement(-1, null, transform, null);
		attackMethod = (damage, velocity) => { Managers.Player.ChangeHealth(damage, sprite); };
	}
}
