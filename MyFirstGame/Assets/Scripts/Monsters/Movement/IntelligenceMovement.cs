using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligenceMovement : IMovement
{
	public int Direction { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }
	public Transform Weapon { get; set; }

	public IntelligenceMovement(int direction, SpriteRenderer sprite, Transform target, Transform triggerTarget, Transform weapon)
	{
		Direction = direction;
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
		Weapon = weapon;
	}

	public Vector2 Move()
	{
		Vector2 spherePos = Target.position;
		spherePos.y += 0.5f;
		spherePos.x += 0.5f * Direction;
		foreach (Collider2D collider in Physics2D.OverlapCircleAll(spherePos, 0.1f))
			if (collider.gameObject.layer == LayerMask.NameToLayer("ground"))
			{
				Direction = -Direction;
				Sprite.flipX = !Sprite.flipX;
				if (Weapon != null)
				{
					Vector2 weaponPos = Weapon.localPosition;
					weaponPos.x = -weaponPos.x;
					Weapon.localPosition = weaponPos;
				}
			}

		Vector2 pos = Target.position + Target.right * Direction;
		return pos;
	}
}
