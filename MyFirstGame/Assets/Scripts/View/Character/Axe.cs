﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour 
{
	private int damage = 1;
	bool dontHit = false;

	#region Unity lifecycle
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag != "character" && !dontHit)
		{
			int direction = CharacterController.flipX ? -1 : 1;
			collision.gameObject.SendMessage("OnHit", new MessageParameters(direction, damage));
			StartCoroutine(Delay());
		}
	}
	#endregion

	IEnumerator Delay()
	{
		dontHit = true;
		yield return new WaitForSeconds(0.5f);
		dontHit = false;
	}
}
