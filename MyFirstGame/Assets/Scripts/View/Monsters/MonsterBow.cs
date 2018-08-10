using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBow : MonoBehaviour 
{
	[SerializeField]
	private SpriteRenderer monsterSprite;
	[SerializeField]
	private GameObject bullet;

	public void Shoot(int damage, float charge)
	{
		GameObject newBullet = Instantiate(bullet);
		newBullet.transform.position = transform.position;
		newBullet.transform.right = transform.right;

		Bullet script = newBullet.GetComponent<Bullet>();
		script.parents.Add(transform.parent.gameObject.transform.parent.gameObject);
		Vector2 force = transform.right * charge;
		script.Shoot(damage, force);	}
}
