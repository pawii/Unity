using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour 
{
	float radius = 2f;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach (Collider2D collider in colliders)
				collider.gameObject.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
		}
	}
}