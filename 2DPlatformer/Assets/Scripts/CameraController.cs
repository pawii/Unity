using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	[SerializeField]
	private float speed = 2f;
	[SerializeField]
	private Transform target;

	void Update()
	{
		Vector3 position = target.position;
		position.z = -10;
		transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
	}
}
