using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateToScreenPoint : MonoBehaviour
{
	CharacterController cc;
	float offset;

	void Start()
	{
		offset = transform.localEulerAngles.z;
		cc = GetComponentInParent<CharacterController>();
	}

	void LateUpdate()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;

		if (cc.FlipX)
			newRight *= -1;

		transform.right = newRight;

		Vector3 newAngles = transform.localEulerAngles;
		newAngles.z += offset;
		transform.localEulerAngles = newAngles;	}
}
