using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateToScreenPoint : MonoBehaviour
{
	float offset;

	void Start()
	{
		offset = transform.localEulerAngles.z;
	}

	void LateUpdate()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;

		if (CharacterController.flipX)
			newRight *= -1;

		transform.right = newRight;

		Vector3 newAngles = transform.localEulerAngles;
		newAngles.z += offset;
		transform.localEulerAngles = newAngles;	}
}
