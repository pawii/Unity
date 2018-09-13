using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Profiling;

public class RotateToScreenPoint : MonoBehaviour
{
	float offset;

	#region Unity lifecycle
	void Start()
	{
		offset = transform.localEulerAngles.z;
	}

	void LateUpdate()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;

		if (CharacterController.flipX)
			newRight.x = newRight.x * -1;

		Vector3 newAngel = new Vector3(0, 0, 0);

		if (newRight.y >= 0)
			newAngel.z = Vector2.Angle(new Vector2(1, 0), newRight);
		else
			newAngel.z = -Vector2.Angle(new Vector2(1, 0), newRight);

		newAngel.z += offset;
		transform.localEulerAngles = newAngel;	}
	#endregion
}
