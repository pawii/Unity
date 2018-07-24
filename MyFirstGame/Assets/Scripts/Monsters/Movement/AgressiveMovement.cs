using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveMovement : IMovement
{
	public int Direction { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }
	public Transform Weapon { get; set; }

	public AgressiveMovement(int direction, SpriteRenderer sprite, Transform target, Transform triggerTarget, Transform weapon)
	{
		Direction = direction;
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
		Weapon = weapon;
	}

	public Vector2 Move()
	{
		if (Target.position.x - TriggerTarget.position.x > 0)
		{
			Direction = -1;
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
			Direction = 1;
			Sprite.flipX = true;
			if (Weapon != null)
			{
				Vector2 weaponPos = Weapon.localPosition;
				weaponPos.x = Mathf.Abs(weaponPos.x);
				Weapon.localPosition = weaponPos;
			}
		}
		Vector2 pos = Target.position + Target.right * Direction;
		return pos;
	}
}
