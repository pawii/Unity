using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageParameters
{
	public SpriteRenderer Sprite { get; private set; }
	public int Damage { get; private set; }

	public MessageParameters(SpriteRenderer sprite, int damage)
	{
		Sprite = sprite;
		Damage = damage;
	}
}
