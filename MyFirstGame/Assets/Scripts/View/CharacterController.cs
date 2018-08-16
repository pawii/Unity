using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
	float radius = 2f;

public static bool Lock { get; set; }

	private bool flipX;
	public bool FlipX
	{
	get { return flipX; }
	private set
	{
		if (value == flipX)
			return;
		else
		{
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
				transform.localScale = newScale;

				flipX = !flipX;
			}
		}
	}

	void Awake()
	{
		flipX = false;

		Lock = false;
	}

	void Update()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;
		if (newRight.x< 0 && !flipX)
			FlipX = true;
		else if (newRight.x >= 0 && flipX)
			FlipX = false;


		if (Input.GetKeyDown(KeyCode.W))
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach (Collider2D collider in colliders)
				collider.gameObject.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
		}

	}
}