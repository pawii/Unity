using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public static Transform character { get; private set;}
	public static GameObject lightPrefab { get; private set; }

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
		Managers.Mission.GoToNext();
		//Managers.Mission.GoToNext();
	}

	public static void FinishLevel()
	{
		if (Managers.Player.health > 0)
		{ 
			Managers.Mission.GoToNext();
			character.position = new Vector2(0, 0);
		}
	}

	public static void AddLight()
	{
		Transform bow = character.GetChild(0).GetChild(0);
		if (!bow.Find("Light"))
		{
			Managers.Inventory.ligth = true;

			GameObject light = Instantiate(lightPrefab);
			light.transform.parent = bow;
			light.transform.localPosition = new Vector3(0, 0, -2);
		}
	}

	public static IEnumerator OnGameComplete()
	{
		UIController.NotificationText.Text = "GAME CMPLETE";
		UIController.NotificationText.Active = true;
		yield return new WaitForSeconds(2);
		UIController.NotificationText.Active = false;	}
}
