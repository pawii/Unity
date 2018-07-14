using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField]
	private Text healthLabel;

	[SerializeField]
	private InventoryPoppup poppup;

	[SerializeField]
	private Text levelEnding;

	void Start () 
	{
		poppup.gameObject.SetActive(false);
		levelEnding.gameObject.SetActive(false);
		OnHealthUpdated();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			bool isActive = poppup.gameObject.activeSelf;
			poppup.gameObject.SetActive(!isActive);
			poppup.Refresh();
		}
	}

	void Awake()
	{
		Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
		Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	void Destroy()
	{
		Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
		Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	void OnHealthUpdated()
	{
		string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
		healthLabel.text = message;
	}

	void OnLevelComplete()
	{
		StartCoroutine(LevelComplete());
	}

	IEnumerator LevelComplete()
	{
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "Level ending!";
		yield return new WaitForSeconds(2);
		Managers.Mission.GoToNext();
	}

	void OnLevelFailed()
	{
		StartCoroutine(FailLevel());
	}

	IEnumerator FailLevel()
	{
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "Level failed";
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
	}

	public void OnSaveGame()
	{
		Managers.Data.SaveGameState();
	}

	public void OnLoadGame()
	{
		Managers.Data.LoadGameState();
	}

	void OnGameComplete()
	{
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "You finished the game!";
	}
}
