using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackMovement : IMovement
{
	public Unit Unit { get; set; }
	public Transform Target { get; set; }
	public Transform TriggerTarget { get; set; }
	public Transform Weapon { get; set; }
	public float Velocity { get; set; }
	public float MinCoordY { get; set; }

	public ArcherAttackMovement(Unit unit, Transform target, Transform triggerTarget, Transform weapon,
					   float velocity, float minCoordY)
	{
		Unit = unit;
		Target = target;
		TriggerTarget = triggerTarget;
		Weapon = weapon;
		Velocity = velocity;
		MinCoordY = minCoordY;
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
			offsetAngle = Angle(new Vector2(1, 0), monsterToCharacter);
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
		//if (Unit.FlipX)
		//	newRight.x = -Mathf.Abs(newRight.x);
		if (newRight.y > MinCoordY)
			Weapon.right = newRight;

		// ИЗМЕНЕНИЕ ПОЛОЖЕНИЯ
		Vector3 pos = new Vector3();
		if (directionPoint.y >= 0)
			pos.y = Vector3.Dot(Weapon.right, Target.up);
		else
			pos.y = -Vector3.Dot(Weapon.right, Target.up * -1);
		if (pos.y > MinCoordY)
			if(directionPoint.x >= 0)
				pos.x = -Vector3.Dot(Weapon.right, Target.right);
			else
				pos.x = Vector3.Dot(Weapon.right, Target.right);
		else
			pos.x = Weapon.localPosition.x;

		pos.y = Mathf.Clamp(pos.y, MinCoordY, 1);
		pos.z = 0;
		Weapon.localPosition = pos;


		if (directionPoint.x >= 0)
			Unit.FlipX = true;
		else
			Unit.FlipX = false;


		return Target.position;
	}

	private float Angle(Vector2 start, Vector2 end)
	{
		float angel = Vector2.Angle(start, end);
		angel = angel / 180 * Mathf.PI;
		return angel;
	}
	
}
