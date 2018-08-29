using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligenceMovement : IMovement
{
	public Unit Unit { get; set; }
	public Transform Target { get; set; }

	public IntelligenceMovement(Unit unit, Transform target)
	{
		Unit = unit;
		Target = target;
	}

	public Vector2 Move()
	{
		int direction = Unit.FlipX ? 1 : -1;
		Vector2 spherePos = Target.position;
		spherePos.y += 0.5f;
		spherePos.x += 0.5f * direction;
		foreach (Collider2D collider in Physics2D.OverlapCircleAll(spherePos, 0.1f))
			if (collider.gameObject.layer == LayerMask.NameToLayer("ground"))
				Unit.FlipX = !Unit.FlipX;

		Vector2 pos = Target.position + Target.right * direction;
		return pos;
	}
}
