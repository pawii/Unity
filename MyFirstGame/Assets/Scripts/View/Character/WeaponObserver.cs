using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObserver : MonoBehaviour 
{
	public bool fastSpeed { get; set; }
	public bool isRun { get; set; }

	GameObject archWeapon;
	GameObject meleeWeapon;
	GameObject curObj;

	void Awake()
	{
		fastSpeed = true;
		isRun = false;

		archWeapon = Resources.Load<GameObject>("ArchWeapon");
		meleeWeapon = Resources.Load<GameObject>("MeleeWeapon");
		curObj = null;
	}

	void Destroy()
	{
		if (curObj != null)
			Destroy(curObj);
	}

	public bool GetFlipX()
	{
		return transform.localScale.x < 0;
	}

	public void SetArch()
	{
		if (curObj == null || !curObj.GetComponent<ArchTorso>())
		{
			Destroy();

			curObj = Instantiate(archWeapon) as GameObject;

			if (GetFlipX())
				curObj.transform.localScale = new Vector3(-5, 5, 1);
			else
				curObj.transform.localScale = new Vector3(5, 5, 1);
			curObj.transform.parent = transform.Find("TorsoDown");
			curObj.transform.localPosition = new Vector2(0, 0);

			ArchTorso archTorso = curObj.GetComponent<ArchTorso>();
			archTorso.Observer = this;
		}
	}

	public void SetMelee()
	{
		if (curObj == null || !curObj.GetComponent<MeleeTorso>())
		{
			Destroy();

			curObj = Instantiate(meleeWeapon) as GameObject;

			MeleeTorso meleeTorso = curObj.GetComponent<MeleeTorso>();
			meleeTorso.Observer = this;

			if (GetFlipX())
				curObj.transform.localScale = new Vector3(-5, 5, 1);
			else
				curObj.transform.localScale = new Vector3(5, 5, 1);
			curObj.transform.parent = transform.Find("TorsoDown");
			curObj.transform.localPosition = new Vector2(0, 0);
		}
	}

	// ВЫПОЛНЯЕТ РОЛЬ ФАСАДА

	public bool Equals(GameObject value)
	{
		foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
			if (child.gameObject.Equals(value))
				return true;
		return false;
	}
}
