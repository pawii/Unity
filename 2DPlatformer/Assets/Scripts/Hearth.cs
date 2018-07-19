using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour 
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Character character = collider.GetComponent<Character>();

		if (character)
		{
			character.Lives++;
			Destroy(gameObject);
		}
	}
}
