using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField]
	private Text healthLabel;

	[SerializeField]
	private InventoryPoppup poppup;

	void Start () 
	{
		poppup.gameObject.SetActive(false);
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
	}

	void Destroy()
	{
		Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
	}

	void OnHealthUpdated()
	{
		string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
		healthLabel.text = message;
	}
}
