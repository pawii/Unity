using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour 
{
	private bool locker = false;

    private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!locker && collider.gameObject.tag == "character")
		{ 
			GameController.FinishLevel();
			StartCoroutine(Delay());
		}
	}

    private IEnumerator Delay()
	{
		locker = true;
		yield return new WaitForSeconds(1);
		locker = false;
	}
}
