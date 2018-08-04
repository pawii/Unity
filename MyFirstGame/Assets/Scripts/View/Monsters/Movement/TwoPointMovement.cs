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
	int directionX;

	public TwoPointMovement(SpriteRenderer sprite, Transform target, float xMinPoint, float xMaxPoint)
	{
		Sprite = sprite;
		Target = target;
		Weapon = null;
		XMinPoint = xMinPoint;
		XMaxPoint = xMaxPoint;
		directionX = Sprite.flipX ? 1 : -1;
	}

	public TwoPointMovement(SpriteRenderer sprite, Transform target, float xMinPoint, float xMaxPoint, Transform weapon) :
	this(sprite, target, xMinPoint, xMaxPoint)
	{
		Weapon = weapon;
	}

	public virtual Vector2 Move()
	{
		if (Target.position.x <= XMinPoint)
		{
			directionX = 1;
			Sprite.flipX = true;
			if (Weapon != null && Weapon.localPosition.x < 0)
			{
				Vector2 weaponPos = Weapon.localPosition;
				weaponPos.x = -weaponPos.x;
				Weapon.localPosition = weaponPos;
			}
		}
		else if (Target.position.x >= XMaxPoint)
		{
			directionX = -1;
			Sprite.flipX = false;
			if (Weapon != null && Weapon.localPosition.x > 0)
			{
				Vector2 weaponPos = Weapon.localPosition;
				weaponPos.x = -weaponPos.x;
				Weapon.localPosition = weaponPos;
			}
		}

		Vector2 pos = Target.position + Target.right * directionX;
		return pos;
	}
}
