using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour 
{
	bool locker = false;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (!locker && collider.gameObject.tag == "character")
		{ 
			GameController.FinishLevel();
			StartCoroutine(Delay());
		}
	}

	IEnumerator Delay()
	{
		locker = true;
		yield return new WaitForSeconds(1);
		locker = false;
	}
}
