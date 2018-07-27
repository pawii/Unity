﻿using System.Collections;
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
		damageRate = 1f;

		triggerArea = 10f;

		sprite = GetComponentInChildren<SpriteRenderer>();

		xMinPos = 35f;
		xMaxPos = 45f;
		movement = new BatTwoPointMovement(sprite, transform, xMinPos, xMaxPos, yMinPoint, yMaxPoint);
		triggerMovement = new BatAgressiveMovement(sprite, transform, character, yMinPoint, yMaxPoint);
		attackMovement = new StayInPlaceMovement(sprite, transform, character);
		attackMethod = Damaging;

		getDamagePower = 5;
		rb = GetComponent<Rigidbody2D>();
	}

	void Damaging(int damage, float velocity)
	{
		bullet.transform.position = transform.position + (character.position - transform.position) / 2;
		bullet.transform.up = character.position - transform.position;
        Instantiate(bullet); 
		MessageParameters parameters = new MessageParameters(sprite, damage);
		character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
	}
}