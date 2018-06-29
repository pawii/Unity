using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab;
	private GameObject enemy;
	private float enemyBaseSpeed = 3f;
	private float enemySpeed;

	void Awake()
	{
		enemySpeed = enemyBaseSpeed;
		Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnDestroy()
	{
		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnSpeedChanged(float settingSpeed)
	{
		enemySpeed = enemyBaseSpeed * settingSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy == null) {
			enemy = Instantiate (enemyPrefab) as GameObject;
			if (enemy != null) {
				enemy.transform.position = new Vector3 (0,1,0);
				enemy.transform.Rotate (0, Random.Range (0, 360), 0);
				enemy.gameObject.GetComponent<WanderingAI>().SetSpeed(enemySpeed);
			}
		}

	}
}
