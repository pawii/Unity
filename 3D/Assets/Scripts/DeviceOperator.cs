using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour 
{
	public float radius = 1.5f;

	void Update () 
	{
		if (Input.GetButtonDown("Fire3"))
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
			foreach (Collider collider in hitColliders)
			{
				Vector3 direction = collider.transform.position - transform.position;
				if (Vector3.Dot(transform.forward, direction) > 0.5f)
					collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); 
			}
		}
	}
}
