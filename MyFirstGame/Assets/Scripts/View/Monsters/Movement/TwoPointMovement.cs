using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwoPointMovement : Movement
{
	protected Transform target;
	private float xMinPoint;
	private float xMaxPoint;
	int directionX;

	public TwoPointMovement(bool flipX, Transform target, float xMinPoint, float xMaxPoint)
	{
		this.flipX = flipX;
		this.target = target;
		this.xMinPoint = xMinPoint;
		this.xMaxPoint = xMaxPoint;
		directionX = flipX ? 1 : -1;
	}

	public override Vector3 Move()
	{
		if (target.position.x <= xMinPoint)
		{
			directionX = 1;
			FlipX = true;
		}
		else if (target.position.x >= xMaxPoint)
		{
			directionX = -1;
			FlipX = false;
		}

		Vector3 pos = target.position + target.right * directionX;
		return pos;
	}
}
