using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {
	public enum Axis
	{
		MouseX = 1, MouseY
	}
	public Axis axis = Axis.MouseX;
	public float speed = 10f;
	public float minVert = -45f;
	public float maxVert = 45f;

	private float mouseY = 0f;
	private float mouseX = 0f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		mouseX = transform.localEulerAngles.y;
		mouseY = transform.localEulerAngles.x;

		rb = GetComponent<Rigidbody> ();
		if (rb != null)
			rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (axis == Axis.MouseX) {
			mouseX += Input.GetAxis ("Mouse X") * speed;
			transform.localEulerAngles = new Vector3 (0, mouseX, 0);
		} else if (axis == Axis.MouseY) {
			mouseY -= Input.GetAxis ("Mouse Y") * speed;
			mouseY = Mathf.Clamp (mouseY, minVert, maxVert);
			transform.localEulerAngles = new Vector3(mouseY, transform.localEulerAngles.y, 0);
		}
		
	}
}
