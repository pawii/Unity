using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Movement 
{
	protected bool flipX;
	public event Action<bool> ChangeFlipX;
	protected bool FlipX
	{
		get { return flipX; }
		set
		{
			flipX = value;
			ChangeFlipX(value);
		}
	}

	public virtual Vector3 Move()
	{
		return Vector3.zero;
	}
}
