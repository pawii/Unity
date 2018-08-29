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
		damageArea = 3;
		damageRate = 3f;

		triggerArea = 10f;

		character = GameController.character;

		calmMovement = new BatTwoPointMovement(this, transform, xMinPos, xMaxPos, yMinPoint, yMaxPoint);
		triggerMovement = new BatAgressiveMovement(this, transform, character, yMinPoint, yMaxPoint);
		attackMovement = new StayInPlaceMovement(this, transform, character);
		attackMethod = Damaging;

		getDamagePower = 5;
		rb = GetComponent<Rigidbody2D>();
	}

	void Damaging(int damage, float velocity)
	{
		bullet.transform.position = transform.position + (character.position - transform.position) / 2;
		bullet.transform.up = character.position - transform.position;
        Instantiate(bullet);
		MessageParameters parameters = new MessageParameters(Methods.GetDirection(gameObject), damage);
		character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
	}
}
