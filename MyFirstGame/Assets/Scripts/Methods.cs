using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Methods
{
	public static float Angle(Vector2 start, Vector2 end)
	{
		float angel = Vector2.Angle(start, end);
		angel = angel / 180 * Mathf.PI;
		return angel;	}
}