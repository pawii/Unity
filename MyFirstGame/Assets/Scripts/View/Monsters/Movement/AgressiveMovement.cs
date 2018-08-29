using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveMovement : IMovement
{
	public Unit Unit { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }

	public AgressiveMovement(Unit unit, Transform target, Transform triggerTarget)
	{
		Unit = unit;
		Target = target;
		TriggerTarget = triggerTarget;	}

	public virtual Vector2 Move()
	{
		int direction = Unit.FlipX ? 1 : -1;
		if (Target.position.x - TriggerTarget.position.x > 0)
			Unit.FlipX = false;
		else
			Unit.FlipX = true;
		Vector2 pos = Target.position + Target.right * direction;
		return pos;
	}
}
