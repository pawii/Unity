using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageParameters
{
	public int Direction { get; private set; }
	public int Damage { get; private set; }

	public MessageParameters(int direction, int damage)
	{
		Direction = direction;
		Damage = damage;
	}
}
