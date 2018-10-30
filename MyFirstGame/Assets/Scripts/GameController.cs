using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour 
{
	[SerializeField]
	private Transform ch;
	public static Transform Character { get; private set;}
	private static GameObject lightPrefab;
	private static int startLevel = 1;
	private static GameObject light;
    
	public static event Action<string> ShowNotification;
	public static event Action RemoveNotification;
	public static event Action RefreshLives;

	private void Awake()
	{
		Character = ch;

		lightPrefab = Resources.Load("Light") as GameObject;

        Managers.ManagersStarted += OnManagersStarted;
        Managers.Mission.LevelLoad += OnLevelLoad;
    }

	private void OnDestroy()
	{
        Managers.ManagersStarted -= OnManagersStarted;
        Managers.Mission.LevelLoad -= OnLevelLoad;
    }

	public static IEnumerator ReloadGame()
	{
		ShowNotification("LEVEL FAILED");
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
		Managers.Player.Reload();
		RefreshLives();
		RemoveNotification();
	}

	public static void ChangeHealth(int value)
	{
		Managers.Player.ChangeHealth(value);
		RefreshLives();
	}

	private void OnManagersStarted()
    {
        Managers.Mission.LoadMainScene();
	}

    public static void LoadLevel(int level)
    {
        ShowNotification("PLEASE, WAIT");

        Managers.Mission.LoadLevel(level);
    }

	public static void FinishLevel()
	{
		ShowNotification("PLEASE, WAIT");

		Managers.Mission.GoNext();
		RemoveLight();
	}

	public static void OnLevelLoad()
	{
		RemoveNotification();
	}

	public static void AddLight()
	{
		if (!Managers.Player.HasLigth)
		{
			Managers.Player.HasLigth = true;

			light = Instantiate(lightPrefab);
			light.transform.parent = Character;
			light.transform.localPosition = new Vector3(0, 0, -2);
		}
	}

	static void RemoveLight()
	{
		if (Managers.Player.HasLigth)
		{
			Managers.Player.HasLigth = false;
			Destroy(light);
		}
	}

	public static void OnGameComplete()
	{
		ShowNotification("GAME COMPLETE");
	}
}
