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


		movement = new TwoPointMovement(sprite, transform, xMinPos, xMaxPos);
		triggerMovement = new AgressiveMovement(sprite, transform, character);
		attackMovement = new StayInPlaceMovement(sprite, transform, character);
		attackMethod = Damaging;

		getDamagePower = 5;
		rb = GetComponent<Rigidbody2D>();
	}

	void Damaging(int damage, float velocity)
	{
		MessageParameters parameters = new MessageParameters(sprite, damage);
		character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);	}
}
