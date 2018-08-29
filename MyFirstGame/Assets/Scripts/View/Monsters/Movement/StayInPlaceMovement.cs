using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInPlaceMovement : IMovement
{
	public Unit Unit { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }

	public StayInPlaceMovement(Unit unit, Transform target, Transform triggerTarget)
	{
		Unit = unit;
		Target = target;
		TriggerTarget = triggerTarget;
	}

	public Vector2 Move()
	{
		if (Target.position.x - TriggerTarget.position.x >= 0)
			Unit.FlipX = false;
		else
			Unit.FlipX = true;
		
		Vector2 pos = Target.position;
		return pos;	}
}
