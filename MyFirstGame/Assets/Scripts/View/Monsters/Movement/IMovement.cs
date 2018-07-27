using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
	SpriteRenderer Sprite { get; set; }
	Transform Target { get; set; }
	Vector2 Move();
}
