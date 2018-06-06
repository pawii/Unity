using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

	public int healPoint = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hurth(int damage)
	{
		healPoint -= damage;
		Debug.Log (healPoint);
	}
}
