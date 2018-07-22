using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
	int Direction { get; set; }
	SpriteRenderer Sprite { get; set; }
	Transform Target { get; set; }
	Transform TriggerTarget { get; set; }
	Vector2 Move();
}
