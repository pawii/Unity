using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligenceMovement : IMovement
{
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform Weapon { get; set; }

	public IntelligenceMovement(SpriteRenderer sprite, Transform target)
	{
		Sprite = sprite;
		Target = target;
		Weapon = null;
	}

	public IntelligenceMovement(SpriteRenderer sprite, Transform target, Transform weapon) : this(sprite, target)
	{
		Weapon = weapon;
	}

	public Vector2 Move()
	{
		int direction = Sprite.flipX ? 1 : -1;
		Vector2 spherePos = Target.position;
		spherePos.y += 0.5f;
		spherePos.x += 0.5f * direction;
		foreach (Collider2D collider in Physics2D.OverlapCircleAll(spherePos, 0.1f))
			if (collider.gameObject.layer == LayerMask.NameToLayer("ground"))
			{
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
