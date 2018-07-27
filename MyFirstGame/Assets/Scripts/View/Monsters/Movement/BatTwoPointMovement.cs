using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTwoPointMovement : TwoPointMovement 
{
	public float YMinPoint { get; set; }
	public float YMaxPoint { get; set; }
	private float amplitude;
	private float avgPoint;
	private int direction;
	
	public BatTwoPointMovement(SpriteRenderer sprite, Transform target, float xMinPoint, float xMaxPoint, 
	                           float yMinPoint, float yMaxPoint) : base(sprite, target, xMinPoint, xMaxPoint)
	{
		YMinPoint = yMinPoint;
		YMaxPoint = yMaxPoint;
		SetAmplitude();
		avgPoint = YMinPoint + (YMaxPoint - YMinPoint) / 2f;
		direction = 1;
	}

	public override Vector2 Move()
	{
		Vector2 pos = base.Move();

		if (pos.y >= (avgPoint + amplitude))
		{
			direction = -1;
		}
		else if (pos.y <= (avgPoint - amplitude))
		{
			direction = 1;
		}
		else if (pos.y <= (avgPoint + 0.1f) && pos.y >= (avgPoint - 0.1f))
		{
			SetAmplitude();
		}
		pos.y += Target.up.y* direction;

		return pos;
	}

	private void SetAmplitude()
	{
		amplitude = ((YMaxPoint - YMinPoint) / 2f) * Random.Range(0.3f, 1f);
	}
}
