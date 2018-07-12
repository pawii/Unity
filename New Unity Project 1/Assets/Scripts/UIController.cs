using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField]
	private Text scoreLabel;
	[SerializeField]
	private SttingsPopup pop;

	private int score;

	void Start () 
	{
		score = 0;
		scoreLabel.text = "Score: " + score;

		pop.gameObject.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			bool isShowing = pop.gameObject.activeSelf;
			pop.gameObject.SetActive(!isShowing);

			if (isShowing)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}


	void Awake()
	{		Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void OnEnemyHit()
	{
		score++;
		scoreLabel.text = "Score: " + score;
	}
}
