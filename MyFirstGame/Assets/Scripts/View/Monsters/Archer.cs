using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Monster 
{
	public float minCoordY;
	private Transform bowTransform;
	private MonsterBow bowScript;

	void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 20f;
		damageRate = 5f;

		triggerArea = 20f;

		character = GameController.character;
		sprite = GetComponentInChildren<SpriteRenderer>();
		bowScript = GetComponentInChildren<MonsterBow>();
		bowTransform = bowScript.gameObject.transform;

		velocity = Mathf.Sqrt(damageArea* Physics2D.gravity.magnitude);
		minCoordY = -0.5f;

		calmMovement = new TwoPointMovement(sprite, transform, xMinPos, xMaxPos, bowTransform);
		triggerMovement = new AgressiveMovement(sprite, transform, character, bowTransform);
		attackMovement = new ArcherAttackMovement(sprite, transform, character, bowTransform, velocity, minCoordY);
		attackMethod = bowScript.Shoot;

		getDamagePower = 5;
		rb = GetComponent<Rigidbody2D>();	}
}
