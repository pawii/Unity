using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletFactory : MonoBehaviour
{
	static GameObject arrowPrefab;
	static GameObject batBulletPrefab;
	static GameObject arrowLightPrefab;

	static BulletFactory()
	{
		arrowPrefab = Resources.Load<GameObject>("Arrow");
		batBulletPrefab = Resources.Load<GameObject>("BatBullet");
		arrowLightPrefab = Resources.Load<GameObject>("ArrowLight");
	}

	public static GameObject CreateArrow(Transform sender, int damage, Vector2 force, string parentTag)
	{
		GameObject arrow = Instantiate(arrowPrefab);
		arrow.transform.position = sender.position;
		arrow.transform.right = force.x < 0 ? -sender.right : sender.right;

		Bullet script = arrow.GetComponent<Bullet>();

		script.Shoot(damage, force, parentTag);

		return arrow;
	}

	public static GameObject CreateArrowWithLight(Transform sender, int damage, Vector2 force, string parentTag)
	{
		GameObject lightParent = CreateArrow(sender, damage, force, parentTag);

		GameObject light = Instantiate(arrowLightPrefab);
		light.transform.parent = lightParent.transform;
		light.transform.localPosition = new Vector3(0, 0, -1);
		return lightParent;
	}

	public static void CreateBatBullet(Vector3 senderPosition, int damage, int direction)
	{
		GameObject bullet = Instantiate(batBulletPrefab);
		bullet.transform.position = senderPosition + (GameController.character.position - senderPosition) / 2;
		bullet.transform.up = GameController.character.position - senderPosition;
		MessageParameters parameters = new MessageParameters(direction, damage);
		GameController.character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
	}
}