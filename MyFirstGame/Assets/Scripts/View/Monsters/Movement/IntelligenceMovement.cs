using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntelligenceMovement : Movement
{
	private Transform target;

	public IntelligenceMovement(bool flipX, Transform target)
	{
		this.flipX = flipX;
		this.target = target;
	}

	public override Vector3 Move()
	{
		int direction = FlipX ? 1 : -1;
		Vector3 spherePos = target.position;
		spherePos.y += 0.5f;
		spherePos.x += 0.5f * direction;
		foreach (Collider2D collider in Physics2D.OverlapCircleAll(spherePos, 0.1f))
			if (collider.gameObject.layer == LayerMask.NameToLayer("ground"))
				FlipX = !FlipX;

		Vector3 pos = target.position + target.right * direction;
		return pos;
	}
}
