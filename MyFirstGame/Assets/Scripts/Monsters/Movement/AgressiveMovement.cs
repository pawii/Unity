using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveMovement : IMovement
{
	public int Direction { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }

	public AgressiveMovement(int direction, SpriteRenderer sprite, Transform target, Transform triggerTarget)
	{
		Direction = direction;
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
	}

	public Vector2 Move()
	{
		if (Target.position.x - TriggerTarget.position.x > 0)
		{
			Direction = -1;
			Sprite.flipX = false;
		}
		else
		{
			Direction = 1;
			Sprite.flipX = true;
		}
		Vector2 pos = Target.position + Target.right * Direction;
		return pos;
	}
}
