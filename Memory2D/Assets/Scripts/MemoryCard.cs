﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {

	[SerializeField] private GameObject cardBack;
	[SerializeField] private SceneController sceneController;
	private int _id;

	public int id {get { return _id;} }
	public void SetCard(int id, Sprite image){
		_id = id;
		GetComponent<SpriteRenderer> ().sprite = image;
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown(){
		if(cardBack.activeSelf)
			sceneController.OpenCard (GetComponent<MemoryCard>());
	}

	public void SetCardState(bool isOpen){
		cardBack.SetActive (!isOpen);
	}
}
