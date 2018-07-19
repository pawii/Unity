using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bow : MonoBehaviour 
{
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private GameObject character;

	private SpriteRenderer characterSprite;
	public float minCoordY = 0;

	private float time1;
	private float time2;
	public float shotPower = 7f;

	void Start()
	{
		characterSprite = character.GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		// ПОВОРОТ
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;
		if (newRight.y > minCoordY)
			transform.right = newRight;

		// ИЗМЕНЕНИЕ ПОЛОЖЕНИЯ
		Vector3 pos = new Vector3();
		pos.y = Vector3.Dot(transform.right, character.transform.up);
		if (pos.y > minCoordY)
			pos.x = Vector3.Dot(transform.right, character.transform.right);
		else
			pos.x = transform.localPosition.x;
		
		if (pos.x < 0)
			characterSprite.flipX = true;
		else
			characterSprite.flipX = false;

		pos.y = Mathf.Clamp(pos.y, 0, 1);
		pos.z = 0;
		transform.localPosition = pos;

		// ВЫСТРЕЛ
		if (Input.GetMouseButtonDown(0))
			time1 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;

		if (Input.GetMouseButtonUp(0))
		{
			time2 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;
			if (time2 < time1)
				time2 += 60;
			float charge = time2 - time1;
			charge = Mathf.Clamp(charge, 0.5f, 2); // ОГРАНИЧЕНИЕ ПО ВРЕМЕНИ ЗАРЯДА (СЕК)
			charge *= shotPower;
			Shoot(charge);
		}
		
	}

	private void Shoot(float charge)
	{
		GameObject newBullet = Instantiate(bullet);
		newBullet.transform.position = transform.position;
		newBullet.transform.rotation = transform.rotation;

		Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
		Vector2 force = transform.right * charge;
		rb.AddForce(force, ForceMode2D.Impulse);
	}
}
