using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInPlaceMovement : IMovement
{
	public int Direction { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }

	public StayInPlaceMovement(int direction, SpriteRenderer sprite, Transform target, Transform triggerTarget)
	{
		Direction = direction;
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
	}

	public Vector2 Move()
	{
		if (Target.position.x - TriggerTarget.position.x >= 0)
			Sprite.flipX = false;
		else
			Sprite.flipX = true;
		
		Vector2 pos = Target.position;
		return pos;	}
}
