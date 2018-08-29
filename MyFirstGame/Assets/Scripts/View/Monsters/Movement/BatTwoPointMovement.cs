using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTwoPointMovement : TwoPointMovement 
{
	public float YMinPoint { get; set; }
	public float YMaxPoint { get; set; }
	private float amplitude;
	private float avgPoint;
	private int directionY;
	
	public BatTwoPointMovement(Unit unit, Transform target, float xMinPoint, float xMaxPoint, 
	                           float yMinPoint, float yMaxPoint) : base(unit, target, xMinPoint, xMaxPoint)
	{
		YMinPoint = yMinPoint;
		YMaxPoint = yMaxPoint;
		SetAmplitude();
		avgPoint = YMinPoint + (YMaxPoint - YMinPoint) / 2f;
		directionY = 1;
	}

	public override Vector2 Move()
	{
		Vector2 pos = base.Move();

		if (pos.y >= (avgPoint + amplitude))
		{
			directionY = -1;
		}
		else if (pos.y <= (avgPoint - amplitude))
		{
			directionY = 1;
		}
		else if (pos.y <= (avgPoint + 0.1f) && pos.y >= (avgPoint - 0.1f))
		{
			SetAmplitude();
		}
		pos.y += Target.up.y* directionY;

		return pos;
	}

	private void SetAmplitude()
	{
		amplitude = ((YMaxPoint - YMinPoint) / 2f) * Random.Range(0.3f, 1f);
	}
}
