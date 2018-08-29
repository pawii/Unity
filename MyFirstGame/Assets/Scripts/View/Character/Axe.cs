using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour 
{
	public WeaponObserver Observer { private get; set; }
	int damage = 1;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag != "character")
		{
			int direction = Observer.GetFlipX() ? -1 : 1;
			collider.gameObject.SendMessage("OnHit", new MessageParameters(direction, damage));
		}
	}
}
