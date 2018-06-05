using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	private WanderingAI ai;

	// Use this for initialization
	void Start () {
		ai = GetComponent<WanderingAI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReactToHit()
	{
		ai.SetAlive (false);
		StartCoroutine (Die());
	}

	private IEnumerator Die()
	{
		transform.Rotate (-75, 0, 0);
		yield return new WaitForSeconds (1.5f);
		Destroy (this.gameObject);
	}
}
