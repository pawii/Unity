using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveMovement : IMovement
{
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }
	public Transform Weapon { get; set; }

	public AgressiveMovement(SpriteRenderer sprite, Transform target, Transform triggerTarget)
	{
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
		Weapon = null;	}

	public AgressiveMovement(SpriteRenderer sprite, Transform target, Transform triggerTarget, Transform weapon) 
		: this(sprite, target, triggerTarget)
	{
		Weapon = weapon;
	}

	public Vector2 Move()
	{
		int direction = Sprite.flipX ? 1 : -1;
		if (Target.position.x - TriggerTarget.position.x > 0)
		{
			Sprite.flipX = false;
			if (Weapon != null)
			{
				Vector2 weaponPos = Weapon.localPosition;
				weaponPos.x = -Mathf.Abs(weaponPos.x);
				Weapon.localPosition = weaponPos;
			}
		}
		else
		{
			Sprite.flipX = true;
			if (Weapon != null)
			{
				Vector2 weaponPos = Weapon.localPosition;
				weaponPos.x = Mathf.Abs(weaponPos.x);
				Weapon.localPosition = weaponPos;
			}
		}
		Vector2 pos = Target.position + Target.right * direction;
		return pos;
	}
}
