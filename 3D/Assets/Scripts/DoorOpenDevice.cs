using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour {

	[SerializeField]
	private Vector3 dPos;

	private bool isOpen = false;

	public void Operate()
	{
		if (!isOpen)
		{
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
		}
		else
		{
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
		}
		isOpen = !isOpen;
	}

	public void Activate()
	{
		if (!isOpen)
		{
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
			isOpen = !isOpen;
		}
	}

	public void Deactivate()
	{
		if (isOpen)
		{
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
			isOpen = !isOpen;
		}	}
}
