using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour 
{
	bool isHided;

	void Awake()
	{
		isHided = false;
	}

	public void Operate()
	{
		isHided = !isHided;
		if (isHided)
		{
			CharacterController.Lock = true;
			Messenger.Broadcast(GameEvent.CHARACTER_HIDED);
		}
		else
		{
			CharacterController.Lock = false;
			Messenger.Broadcast(GameEvent.CHARACTER_SEEMED);
		}
	}
}