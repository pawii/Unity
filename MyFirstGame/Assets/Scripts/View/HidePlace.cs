﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour 
{
	bool isHided;
	List<Monster> monsters;

	void Awake()
	{
		monsters = new List<Monster>();
		isHided = false;
	}

	public void Operate()
	{
		Debug.Log("OK");
		isHided = !isHided;
		if (isHided)
			CharacterController.Lock = true;
		else
			CharacterController.Lock = false; 
		Notify();
	}

	public void Attach(Monster monster)
	{
		if (!monsters.Contains(monster))
			monsters.Add(monster);
	}

	public void Detach(Monster monster)
	{
		if (monsters.Contains(monster))
			monsters.Remove(monster);
	}

	void Notify()
	{
		if(isHided)
				monster.OnCharacterHided();
		else
			foreach (Monster monster in monsters)
				monster.OnCharacterSeemed();
	}
}