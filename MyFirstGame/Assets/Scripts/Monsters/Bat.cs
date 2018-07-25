using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster 
{
	[SerializeField]
	private GameObject bullet;
	public float yMinPoint;
	public float yMaxPoint;

	void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 5;
		damageRate = 1f;

		triggerArea = 10f;

		sprite = GetComponentInChildren<SpriteRenderer>();

		xMinPos = 2f;
		xMaxPos = 10f;
		movement = new BatTwoPointMovement(sprite, transform, xMinPos, xMaxPos, yMinPoint, yMaxPoint);
		triggerMovement = new BatAgressiveMovement(sprite, transform, character, yMinPoint, yMaxPoint);
		attackMovement = new StayInPlaceMovement(sprite, transform, character);
		attackMethod = Damaging;
	}

	void Damaging(int damage, float velocity)
	{
		bullet.transform.position = transform.position + (character.position - transform.position) / 2;
		bullet.transform.up = character.position - transform.position;
		Instantiate(bullet);
	}
}
