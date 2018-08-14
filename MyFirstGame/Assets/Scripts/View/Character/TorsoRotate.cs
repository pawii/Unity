using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoRotate : MonoBehaviour 
{
	void Update () 
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;

		Vector3 newAngel = transform.localEulerAngles;

		if (newRight.y< 0)
			newAngel.z = -45 * Mathf.Sin(Methods.Angle(newRight, new Vector2(1, 0)));
		else
			newAngel.z = 45 * Mathf.Sin(Methods.Angle(newRight, new Vector2(1, 0)));

		transform.localEulerAngles = newAngel;
		
	}
}
