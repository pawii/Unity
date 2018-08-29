using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointMovement : IMovement
{
	public Unit Unit { get; set; }
	public Transform Target { get; set; }
	public float XMinPoint { get; set; }
	public float XMaxPoint { get; set; }
	int directionX;

	public TwoPointMovement(Unit unit, Transform target, float xMinPoint, float xMaxPoint)
	{
		Unit = unit;
		Target = target;
		XMinPoint = xMinPoint;
		XMaxPoint = xMaxPoint;
		directionX = Unit.FlipX ? 1 : -1;
	}

	public virtual Vector2 Move()
	{
		if (Target.position.x <= XMinPoint)
		{
			directionX = 1;
			Unit.FlipX = true;
		}
		else if (Target.position.x >= XMaxPoint)
		{
			directionX = -1;
			Unit.FlipX = false;
		}

		Vector2 pos = Target.position + Target.right * directionX;
		return pos;
	}
}
