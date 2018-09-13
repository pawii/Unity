using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour 
{
	[SerializeField]
	private Transform ch;
	public static Transform character { get; private set;}
	private static GameObject lightPrefab;
	private static int startLevel = 1;
	static GameObject light;

	public static event Action<string> ShowNotification;
	public static event Action RemoveNotification;
	public static event Action RefreshLives;

	private void Awake()
	{
		character = ch;

		lightPrefab = Resources.Load("Light") as GameObject;

		Managers.Mission.LevelLoad += OnLevelLoad;
	}

	private void Destroy()
	{
		Managers.Mission.LevelLoad -= OnLevelLoad;
	}

	public static IEnumerator ReloadGame()
	{
		ShowNotification("LEVEL FAILED");
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
		Managers.Player.Reload();
		RefreshLives();
		RemoveNotification();	}

	public static void ChangeHealth(int value)
	{
		Managers.Player.ChangeHealth(value);
		RefreshLives();
	}

	public static void OnManagersStarted()
	{
		ShowNotification("PLEASE, WAIT");
		for (int i = 0; i < startLevel; i++)
			Managers.Mission.GoNext();
		//Application.LoadLevel("TestSprites");
	}

	public static void FinishLevel()
	{
		ShowNotification("PLEASE, WAIT");

		Managers.Mission.GoNext();
		RemoveLight();
	}

	private void OnLevelLoad()
	{
		RemoveNotification();
	}

	public static void AddLight()
	{
		if (!Managers.Inventory.ligth)
		{
			Managers.Inventory.ligth = true;

			light = Instantiate(lightPrefab);
			light.transform.parent = character;
			light.transform.localPosition = new Vector3(0, 0, -2);
		}
	}

	static void RemoveLight()
	{
		if (Managers.Inventory.ligth)
		{
			Managers.Inventory.ligth = false;
			Destroy(light);
		}
	}

	public static void OnGameComplete()
	{
		ShowNotification("GAME COMPLETE");	}
}
