using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
	public float radius = 3.5f;
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Transform player = GameObject.FindWithTag("Player").transform;
			if (Vector3.Distance(player.position, transform.position) <= radius)
			{
				if (Vector3.Dot(transform.position - player.position, player.forward) > 0.5f)
				{
					Operate();
				}
			}
		}	
	}

	public virtual void Operate()
	{
	}
}
