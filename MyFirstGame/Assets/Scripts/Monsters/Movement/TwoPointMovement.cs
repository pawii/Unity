using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointMovement : IMovement
{
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform Weapon { get; set; }
	public float XMinPoint { get; set; }
	public float XMaxPoint { get; set; }

	public TwoPointMovement(SpriteRenderer sprite, Transform target, float xMinPoint, float xMaxPoint)
	{
		Sprite = sprite;
		Target = target;
		Weapon = null;
		XMinPoint = xMinPoint;
		XMaxPoint = xMaxPoint;
	}

	public TwoPointMovement(SpriteRenderer sprite, Transform target, float xMinPoint, float xMaxPoint, Transform weapon) :
	this(sprite, target, xMinPoint, xMaxPoint)
	{
		Weapon = weapon;
	}

	public virtual Vector2 Move()
	{
		int direction = Sprite.flipX ? 1 : -1;
		if (Target.position.x <= XMinPoint || Target.position.x >= XMaxPoint)
		{
			direction *= -1;
			Sprite.flipX = !Sprite.flipX;
			if (Weapon != null)
			{
				Vector2 weaponPos = Weapon.localPosition;
				weaponPos.x = -weaponPos.x;
				Weapon.localPosition = weaponPos;
			}
		}

		Vector2 pos = Target.position + Target.right * direction;
		return pos;
	}
}
