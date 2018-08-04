using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour 
{
	public float radius = 2f;

	public void Operate()
	{
		GameController.AddLight();
	}
}
