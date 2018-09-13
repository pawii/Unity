using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Methods
{
	public static float Angle(Vector2 start, Vector2 end)
	{
		float angel = (float)Vector2.Angle(start, end);
		angel = (float)angel / 180f * (float)Mathf.PI;
		return angel;	}

	public static int GetDirection(GameObject gameObject)
	{
		return gameObject.transform.localScale.x > 0 ? 1 : -1;
	}
}