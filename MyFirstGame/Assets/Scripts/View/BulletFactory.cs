using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletFactory : MonoBehaviour 
{
	static GameObject arrowPrefab;

	static BulletFactory()
	{
		arrowPrefab = Resources.Load<GameObject>("Arrow");
	}

	public static void CreateArrow(Transform sender, int damage, Vector2 force, Func<GameObject, bool> equals,bool hasLight)
	{
		GameObject arrow = Instantiate(arrowPrefab);
		arrow.transform.position = sender.position;
		arrow.transform.right = force.x < 0 ? -sender.right : sender.right;

		if (Managers.Inventory.ligth)
		{
			GameObject light = Instantiate(GameController.lightPrefab);
			light.transform.parent = arrow.transform;
			light.transform.localPosition = new Vector3(0, 0, -1);
		}

		Bullet script = arrow.GetComponent<Bullet>();
		script.parentEquals = equals;

		script.Shoot(damage, force);
	}
}
