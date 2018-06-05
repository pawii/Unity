using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

	public float speed = 3f;
	public float obstacleRange = 5f;

	private bool isAlive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isAlive) {
			transform.Translate (0, 0, speed * Time.deltaTime);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast (ray, 0.75f, out hit))
			if (hit.distance < obstacleRange)
				transform.Rotate (0, Random.Range(-110, 110),0);
		}
	}

	public void SetAlive(bool isAlive){
		this.isAlive = isAlive;
	}
}
