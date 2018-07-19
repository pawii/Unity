using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{
	[SerializeField]
	private float rate = 2f;

	private Bullet bullet;
	[SerializeField]
	private Color color = Color.green;

	void Awake()
	{
		bullet = Resources.Load<Bullet>("Bullet");
	}

	void Start()
	{
		InvokeRepeating("Shoot", rate, rate);
	}

	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		Unit unit = collider.GetComponent<Unit>();

		if (unit && unit is Character)
		{
			if (Mathf.Abs(transform.position.x - unit.transform.position.x) < 0.5f) ReceiveDamage();
			else unit.ReceiveDamage();
		}
	}

	private void Shoot()
	{
		Vector3 pos = transform.position;
		pos.y += 0.5f;
		Bullet newBullet = Instantiate(bullet, pos, bullet.transform.rotation) as Bullet;

		newBullet.Parent = gameObject;
		newBullet.Direction = -newBullet.transform.right;
		newBullet.Color = color;
	}
}
