using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgressiveMovement : Movement
{
	protected Transform target;
	private Transform triggerTarget;

	public AgressiveMovement(bool flipX, Transform target, Transform triggerTarget)
	{
		this.flipX = flipX;
		this.target = target;
		this.triggerTarget = triggerTarget;	}

	public override Vector3 Move()
	{
		int direction = FlipX ? 1 : -1;
		if (target.position.x - triggerTarget.position.x > 0)
			FlipX = false;
		else
			FlipX = true;
		Vector3 pos = target.position + target.right * direction;
		return pos;
	}
}
