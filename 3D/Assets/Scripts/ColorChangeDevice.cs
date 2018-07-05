using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour {


	void Start () 
	{
		Operate();
	}

	public void Operate()
	{
		Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		GetComponent<Renderer>().material.color = color;
	}
}
