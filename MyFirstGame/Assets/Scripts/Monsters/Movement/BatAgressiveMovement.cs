using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAgressiveMovement : AgressiveMovement
{
	public float YMinPoint { get; set; }
	public float YMaxPoint { get; set; }
	private float amplitude;
	private float avgPoint;
	private int direction;

	public BatAgressiveMovement(SpriteRenderer sprite, Transform target, Transform triggertarget,
	                            float yMinPoint, float yMaxPoint) : base(sprite, target, triggertarget)
	{
		YMinPoint = yMinPoint;
		YMaxPoint = yMaxPoint;
		SetAmplitude();
		avgPoint = YMinPoint + (YMaxPoint - YMinPoint) / 2f;
		direction = 1;
	}

	public override Vector2 Move()
	{
		Debug.Log(Target.up.y);
		Vector2 pos = base.Move();

		if (pos.y >= (avgPoint + amplitude))
		{
			direction = -1;
			pos.y += Target.up.y * direction * 2;
		}
		else if (pos.y <= (avgPoint - amplitude))
		{
			direction = 1;
			pos.y += Target.up.y * direction * 2;
		}
		else if (pos.y <= (avgPoint + 0.01f) || pos.y >= (avgPoint - 0.01f))
		{
			SetAmplitude();
			pos.y += Target.up.y * direction;
		}

		return pos;
	}

	private void SetAmplitude()
	{
		amplitude = ((YMaxPoint - YMinPoint) / 2f) * Random.Range(0.3f, 1f);
	}
}
