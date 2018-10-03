using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
	private static GameObject archWeapon;
	private static GameObject meleeWeapon;
	private static GameObject curObj;

	static WeaponFactory()
	{
		archWeapon = Resources.Load<GameObject>("ArchWeapon");
		meleeWeapon = Resources.Load<GameObject>("MeleeWeapon");
		curObj = null;
	}

	public static void SetArch(Transform parent, bool flipX)
	{
		if (curObj == null || curObj.tag != "arch")
		{
			Destroy();

			curObj = Instantiate(archWeapon) as GameObject;

			if (flipX)
				curObj.transform.localScale = new Vector3(-5, 5, 1);
			else
				curObj.transform.localScale = new Vector3(5, 5, 1);
			curObj.transform.parent = parent;

			curObj.transform.localPosition = Vector3.zero;
		}
	}

	public static void SetMelee(Transform parent, bool flipX)
	{
		if (curObj == null || curObj.tag != "melee")
		{
			Destroy();

			curObj = Instantiate(meleeWeapon) as GameObject;

			if (flipX)
				curObj.transform.localScale = new Vector3(-5, 5, 1);
			else
				curObj.transform.localScale = new Vector3(5, 5, 1);
			curObj.transform.parent = parent;

			curObj.transform.localPosition = Vector3.zero;
		}
	}

	private static void Destroy()
	{
		if (curObj != null)
			Destroy(curObj);
	}
}
