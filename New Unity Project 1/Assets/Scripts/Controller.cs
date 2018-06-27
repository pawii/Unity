using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab;
	private GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy == null) {
			enemy = Instantiate (enemyPrefab) as GameObject;
			if (enemy != null) {
				enemy.transform.position = new Vector3 (0,1,0);
				enemy.transform.Rotate (0, Random.Range (0, 360), 0);
			}
		}

	}
}
