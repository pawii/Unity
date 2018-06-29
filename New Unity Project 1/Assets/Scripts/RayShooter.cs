﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class RayShooter : MonoBehaviour {

	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) {
			Vector3 point = new Vector3 (camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
			Ray ray = camera.ScreenPointToRay (point);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				ReactiveTarget rt = hit.transform.gameObject.GetComponent<ReactiveTarget>();
				if (rt != null)
				{ rt.ReactToHit(); Messenger.Broadcast(GameEvent.ENEMY_HIT); }
				else
					StartCoroutine (SphereIndicator(hit.point));
			}
		}
	}

	void OnGUI()
	{
		int size = 16;
		Rect rect = new Rect (camera.pixelWidth/2 - size/2, camera.pixelHeight/2 - size/2, size, size);
		GUI.Label (rect, "*");
	}

	private IEnumerator SphereIndicator(Vector3 point)
	{
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = point;
		yield return new WaitForSeconds (1);
		Destroy (sphere);
	}
}
