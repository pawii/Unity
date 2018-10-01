using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	float amplitude;
	int direction;
	float startY;
	float speed;

	void Start()
	{
		amplitude = 0.5f;
		direction = 1;
		startY = transform.position.y;
		speed = 0.5f;
	}

	void Update()
	{
		if (transform.position.y >= startY + amplitude / 2)
			direction = -1;
		else if (transform.position.y <= startY - amplitude / 2)
			direction = 1;
		Vector3 target = transform.position + transform.up * direction;
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "character")
		{
			GameController.ChangeHealth(1);
			Destroy(gameObject);
		}
	}
}
