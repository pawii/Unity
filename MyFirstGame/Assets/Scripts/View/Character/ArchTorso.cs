using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArchTorso : MonoBehaviour
{
	bool fastSpeed { get; set; }
	bool isRun { get; set; }

	public WeaponObserver Observer { private get; set; }
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
			Observer.fastSpeed = false;

			time1 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;
			power = true;

			IsShoot = true;
		}

		if (Input.GetMouseButtonUp(0) && power && !CharacterController.Lock)
		{
			Observer.fastSpeed = true;

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
		/*GameObject newBullet = Instantiate(bullet);
		newBullet.transform.position = arrow.position;

		newBullet.transform.right = arrow.TransformDirection(arrow.right).x < 0 ? -arrow.right : arrow.right;

		if (Managers.Inventory.ligth)
		{
			GameObject light = Instantiate(GameController.lightPrefab);
			light.transform.parent = newBullet.transform;
			light.transform.localPosition = new Vector3(0, 0, -1);
		}

		Bullet script = newBullet.GetComponent<Bullet>();
		script.parentEquals = Observer.Equals;*/
		Vector2 force = Observer.GetFlipX() ? -arrow.right : arrow.right;

		force *= charge;
		BulletFactory.CreateArrow(arrow, 1, force, Observer.Equals, false);
	}

	private IEnumerator ShootDelay()
	{
		requireShoot = false;

		yield return new WaitForSeconds(shootDelay);

		requireShoot = true;
	}
}
