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

		character = GameController.character;

		calmMovement = new TwoPointMovement(this, transform, xMinPos, xMaxPos);
		triggerMovement = new AgressiveMovement(this, transform, character);
		attackMovement = new StayInPlaceMovement(this, transform, character);
		attackMethod = Damaging;

		getDamagePower = 5;
		rb = GetComponent<Rigidbody2D>();
	}

	void Damaging(int damage, float velocity)
	{
		MessageParameters parameters = new MessageParameters(Methods.GetDirection(gameObject), damage);
		character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);	}
}
