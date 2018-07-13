using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : BaseDevice {

	[SerializeField]
	private Vector3 dPos;
	public bool isTrigger;

	private bool isOpen = false;

	public override void Operate()
	{
		if (isTrigger)
			return;
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
