using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackMovement : IMovement
{
	public int Direction { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }

	public GhostAttackMovement(int direction, SpriteRenderer sprite, Transform target, Transform triggerTarget)
	{
		Direction = direction;
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
	}

	// ЗАМЕНИТЬ АНИМАЦИЕЙ
	public Vector2 Move()
	{
		Vector2 pos = Target.position;
		return pos;
	}
	// ЗАМЕНИТЬ АНИМАЦИЕЙ
}