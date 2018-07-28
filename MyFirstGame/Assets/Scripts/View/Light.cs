using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour 
{
	public float radius = 2f;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach (Collider2D collider in colliders)
				if (collider.gameObject.tag == "character")
					GameController.AddLight();
		}
	}
}
