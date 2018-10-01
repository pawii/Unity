using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBullet : MonoBehaviour 
{
	void Start () 
	{
		StartCoroutine(OnStart());	
	}

	IEnumerator OnStart()
	{
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}