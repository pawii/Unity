using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
	Unit Unit { get; set; }
	Transform Target { get; set; }
	Vector2 Move();
}
