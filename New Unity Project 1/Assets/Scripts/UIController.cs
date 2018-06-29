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

	public void OnOpenSettings()
	{
		pop.Open();
	}

	void Start () 
	{
		score = 0;
		scoreLabel.text = "Score: " + score;
		pop.Close();
	}

	void Update () 
	{
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
