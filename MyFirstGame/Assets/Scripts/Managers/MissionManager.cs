﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissionManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }

	private int curLevel;
	private const int maxLevel = 3;

	private GameObject levelPrefab;

    public event Action LevelLoad;

	public void StartUp()
	{
		status = ManagerStatus.Initializing;

        curLevel = 0;

		status = ManagerStatus.Started;
	}

	public void RestartCurrent()
	{
		if (levelPrefab != null)
		{
			Destroy(levelPrefab);
			GC.Collect();
		}
		levelPrefab = Resources.Load<GameObject>("Level" + curLevel);
		levelPrefab = Instantiate(levelPrefab);
		int offset = curLevel * 100;
		levelPrefab.transform.position = new Vector3(0, offset, 0);
		GameController.character.transform.position = new Vector3(0, offset + 4, 0);
		StartCoroutine(Delay());
	}

	public void GoNext()
	{
		if (curLevel < maxLevel)
		{
			curLevel++;
			RestartCurrent();
		}
		else
		{
			GameController.OnGameComplete();
		}
	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds(2);
        LevelLoad();
	}
}
