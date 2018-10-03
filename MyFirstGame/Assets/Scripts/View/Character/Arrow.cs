using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour 
{
	#region Unity lifecycle
	void Awake()
	{
		ArchTorso.Shoot += this.Shoot;
	}

	void OnDestroy()
	{
		ArchTorso.Shoot -= this.Shoot;
	}
	#endregion

	private void Shoot(float charge, int damage)
	{
		Vector3 force = CharacterController.Get_FlipX() ? -transform.right : transform.right;

		force *= charge;

		if (Managers.Player.HasLigth)
			BulletFactory.CreateArrowWithLight(transform, damage, force, tag);
		else
			BulletFactory.CreateArrow(transform, damage, force, tag);
	}
}
