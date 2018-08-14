using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag != "character")
			collider.gameObject.SendMessage("OnHit", null);
	}
}
