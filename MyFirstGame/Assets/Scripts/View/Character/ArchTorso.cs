﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArchTorso : MonoBehaviour
{
	public ArchState Mediator { private get; set; }
	static Animator anim;
	public static bool IsShoot
	{
		private get { return anim.GetBool("shoot"); }
		set
		{ anim.SetBool("shoot", value); }
	}

	Transform arrow;

	[SerializeField]
	private GameObject bullet;
	private float time1;
	private float time2;
	public float shotPower = 7f;
	bool requireShoot = true;
	bool power = false;
	public float shootDelay = 0.5f;

	public int damage = 1;

	void Awake()
	{
		anim = GetComponent<Animator>();

		arrow = transform.Find("RightArm1/RightArm2/Arch/Arrow");

		bullet = Resources.Load<GameObject>("Arrow");
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && requireShoot && !CharacterController.Lock)
		{
			Mediator.fastSpeed = false;

			time1 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;
			power = true;

			IsShoot = true;
		}

		if (Input.GetMouseButtonUp(0) && power && !CharacterController.Lock)
		{
			Mediator.fastSpeed = true;

			time2 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;
			if (time2 < time1)
				time2 += 60;
			float charge = time2 - time1;
			charge = Mathf.Clamp(charge, 0.5f, 2); // ОГРАНИЧЕНИЕ ПО ВРЕМЕНИ ЗАРЯДА (СЕК)
			charge *= shotPower;
			StartCoroutine(ShootDelay());
			Shoot(charge);
			power = false;

			IsShoot = false;
		}
	}

	private void Shoot(float charge)
	{
		GameObject newBullet = Instantiate(bullet);
		newBullet.transform.position = arrow.position;
		newBullet.transform.rotation = arrow.rotation;

		newBullet.transform.right = Mediator.GetFlipX() ? -arrow.right : arrow.right;

		//if (Managers.Inventory.ligth)
		//{
		//	GameObject light = Instantiate(GameController.lightPrefab);
		//	light.transform.parent = newBullet.transform;
		//	light.transform.localPosition = new Vector3(0, 0, -2);
		//}

		Bullet script = newBullet.GetComponent<Bullet>();
		script.parents.Add(arrow.parent.parent.gameObject);
		script.parents.Add(arrow.parent.parent.parent.gameObject);
		Vector2 force = Mediator.GetFlipX() ? -arrow.right : arrow.right;

		force *= charge;
		script.Shoot(damage, force);
	}

	private IEnumerator ShootDelay()
	{
		requireShoot = false;

		yield return new WaitForSeconds(shootDelay);

		requireShoot = true;
	}
}