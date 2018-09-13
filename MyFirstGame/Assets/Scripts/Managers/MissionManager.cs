using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissionManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }

	[SerializeField]
	private int curLevel;
	[SerializeField]
	private int maxLevel;

	GameObject levelPrefab;

	public event Action LevelLoad;

	public void StartUp()
	{
		status = ManagerStatus.Initializing;

        UpdateData(0, 3);

		status = ManagerStatus.Started;
	}

	public void RestartCurrent()
	{

		if (levelPrefab != null)
			Destroy(levelPrefab);
		levelPrefab = Resources.Load<GameObject>("Level" + curLevel);
		levelPrefab = Instantiate(levelPrefab);
		float offset = curLevel * 100;
		levelPrefab.transform.position = new Vector3(0, offset, 0);
		GameController.character.transform.position = new Vector3(0, offset + 4, 0);
		StartCoroutine(Delay());
	}

	public void UpdateData(int curLevel, int maxLevel)
	{
		this.curLevel = curLevel;
		this.maxLevel = maxLevel;
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
