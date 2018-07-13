using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

	[SerializeField] private GameObject fireballPrefab;
	public float baseSpeed = 3f;
	public float obstacleRange = 5f;

	private GameObject fireball;
	private float speed;
	public bool isAlive = true;


	void Awake()
	{
		speed = baseSpeed;
		Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnDestroy()
	{
		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnSpeedChanged(float settingSpeed)
	{
		speed = baseSpeed * settingSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (isAlive) {
			transform.Translate (0, 0, speed * Time.deltaTime);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast (ray, 0.75f, out hit)){
				if(hit.transform.gameObject.GetComponent<CharacterController>()){
					if (fireball == null) {
						fireball = Instantiate (fireballPrefab) as GameObject;
						fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
						fireball.transform.rotation = transform.rotation;
					}
				}
				else if (hit.distance < obstacleRange)
					transform.Rotate (0, Random.Range(-110, 110),0);}
		}
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}
}
