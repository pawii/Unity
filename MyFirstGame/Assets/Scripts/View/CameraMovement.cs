using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public Transform target;
	public float speed = 2f;

	void Start()
	{
		//target = GameController.character;
	}

	void Update()
	{
		Vector3 targetPos = target.position;
		targetPos.z = -10;
		transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
	}
}
