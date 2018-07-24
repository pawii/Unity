using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Monster 
{
	private int minCoordY = 0;
	private Transform bowTransform;
	private MonsterBow bowScript;

	void Awake()
	{
		speed = 3f;
		health = 3;
		damage = -1;

		damageArea = 10f;
		velocity = Mathf.Sqrt(damageArea* Physics2D.gravity.magnitude);

		bowScript = GetComponentInChildren<MonsterBow>();
		bowTransform = bowScript.gameObject.transform;

		damageRate = 1f;
		triggerArea = 15f;
		sprite = GetComponentInChildren<SpriteRenderer>();
		movement = new IntelligenceMovement(-1, sprite, transform, character, bowTransform);
		triggerMovement = new AgressiveMovement(-1, sprite, transform, character, bowTransform);
		attackMovement = new ArcherAttackMovement(-1, sprite, transform, character, bowTransform, velocity, minCoordY);
		attackMethod = bowScript.Shoot;	}
}
