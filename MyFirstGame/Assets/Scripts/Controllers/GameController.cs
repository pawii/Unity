using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	[SerializeField]
	private GameObject[] dontDestroy;
	[SerializeField]
	UIController UI;

	void Awake()
	{
		foreach (GameObject gameObject in dontDestroy)
			DontDestroyOnLoad(gameObject);

		Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
	}

	void Destroy()
	{
		Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
	}

	void OnLevelFailed()
	{
		StartCoroutine(Reload());	
	}

	IEnumerator Reload()
	{
		UI.NotificationText = "LEVEL FAILED";
		UI.NotificationActive = true;
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
		Managers.Player.Reload();
		UI.NotificationActive = false;	}
}
