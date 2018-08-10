using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public static Transform character { get; private set;}
	public static GameObject lightPrefab { get; private set; }
	public static int startLevel = 1;
	static GameObject light;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		character = GetComponentInChildren<CharacterMovement>().gameObject.transform;
		character.position = new Vector2(0, 0);

		lightPrefab = Resources.Load("Light") as GameObject;
	}

	public static IEnumerator ReloadGame()
	{
		UIController.NotificationText.Text = "LEVEL FAILED";
		UIController.NotificationText.Active = true;
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
		Managers.Player.Reload();
		character.position = new Vector2(0, 0);
		UIController.LivesPanel.Refresh();
		UIController.NotificationText.Active = false;	}

	public static void ChangeHealth(int value)
	{
		Managers.Player.ChangeHealth(value);
		UIController.LivesPanel.Refresh();
	}

	public static void OnManagersStarted()
	{
		for (int i = 0; i < startLevel; i++)
			Managers.Mission.GoToNext();
	}

	public static void FinishLevel()
	{
		if (Managers.Player.health > 0)
		{ 
			Managers.Mission.GoToNext();
			character.position = new Vector2(0, 0);
			RemoveLight();
		}
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

	public static IEnumerator OnGameComplete()
	{
		UIController.NotificationText.Text = "GAME CMPLETE";
		UIController.NotificationText.Active = true;
		yield return new WaitForSeconds(2);
		UIController.NotificationText.Active = false;	}
}
