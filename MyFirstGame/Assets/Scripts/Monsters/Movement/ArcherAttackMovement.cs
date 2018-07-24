using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackMovement : IMovement
{
	public int Direction { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }
	public Transform Weapon { get; set; }
	public float Velocity { get; set; }
	public int MinCoordY { get; set; }

	public ArcherAttackMovement(int direction, SpriteRenderer sprite, Transform target, Transform triggerTarget, Transform weapon,
					   float velocity, int minCoordY)
	{
		Direction = direction;
		Sprite = sprite;
		Target = target;
		TriggerTarget = triggerTarget;
		Weapon = weapon;
		Velocity = velocity;
		MinCoordY = minCoordY;

		TriggerTarget = triggerTarget.GetComponentInChildren<SpriteRenderer>().transform;
	}

	public Vector2 Move()
	{
		float S = TriggerTarget.position.x - Weapon.position.x;

		float angle = (Mathf.Asin(S * Physics2D.gravity.magnitude / Mathf.Pow(Velocity, 2))) / 2;
		if (angle< 0)
			angle += Mathf.PI;

		float offsetAngle;
		Vector2 monsterToCharacter = TriggerTarget.position - Weapon.position;
		if (monsterToCharacter.x >= 0 && monsterToCharacter.y >= 0)
		{
			offsetAngle = Angle(monsterToCharacter, new Vector2(1, 0));
			angle += offsetAngle;
		}
		if (monsterToCharacter.x >= 0 && monsterToCharacter.y <= 0)
		{
			offsetAngle = Angle(monsterToCharacter, new Vector2(1, 0));
			angle -= offsetAngle;
		}
		if (monsterToCharacter.x <= 0 && monsterToCharacter.y >= 0)
		{
			offsetAngle = Angle(monsterToCharacter, new Vector2(-1, 0));
			angle -= offsetAngle;
		}
		if (monsterToCharacter.x <= 0 && monsterToCharacter.y <= 0)
		{
			offsetAngle = Angle(monsterToCharacter, new Vector2(-1, 0));
			angle += offsetAngle;
		}

		Vector2 directionPoint = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));


		// ПОВОРОТ
		Vector2 newRight = directionPoint;
		if (newRight.y > MinCoordY)
			Weapon.right = newRight;

		// ИЗМЕНЕНИЕ ПОЛОЖЕНИЯ
		Vector3 pos = new Vector3();
		Debug.Log(Vector3.Dot(Weapon.right, Target.up));
		pos.y = Vector3.Dot(Weapon.right, Target.up);
		if (pos.y > MinCoordY)
			pos.x = Vector3.Dot(Weapon.right, Target.right);
		else
			pos.x = Weapon.localPosition.x;

		pos.y = Mathf.Clamp(pos.y, 0, 1);
		pos.z = 0;
		Weapon.localPosition = pos;


		if (Weapon.localPosition.x >= 0)
			Sprite.flipX = true;
		else
			Sprite.flipX = false;


		return Target.position;
	}

	private float Angle(Vector2 start, Vector2 end)
	{
		float angel = Vector2.Angle(start, end);
		angel = angel / 180 * Mathf.PI;
		return angel;
	}
	
}
