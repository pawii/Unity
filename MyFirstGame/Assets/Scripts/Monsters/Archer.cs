using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Monster 
{
	private int minCoordY;
	private Transform bowTransform;
	private MonsterBow bowScript;

	void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 5f;
		damageRate = 1f;

		triggerArea = 5f;

		sprite = GetComponentInChildren<SpriteRenderer>();
		bowScript = GetComponentInChildren<MonsterBow>();
		bowTransform = bowScript.gameObject.transform;

		velocity = Mathf.Sqrt(damageArea* Physics2D.gravity.magnitude);
		minCoordY = 0;

		movement = new IntelligenceMovement(sprite, transform, bowTransform);
		triggerMovement = new AgressiveMovement(sprite, transform, character, bowTransform);
		attackMovement = new ArcherAttackMovement(sprite, transform, character, bowTransform, velocity, minCoordY);
		attackMethod = bowScript.Shoot;	}
}
