using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

	public void Hurth(int damage)
	{
		Managers.Player.ChangeHealth(-damage);
	}
}
