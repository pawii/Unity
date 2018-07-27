using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public static IEnumerator ReloadGame()
	{
		UIController.NotificationText.Text = "LEVEL FAILED";
		UIController.NotificationText.Active = true;
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
		Managers.Player.Reload();
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
	}
}
