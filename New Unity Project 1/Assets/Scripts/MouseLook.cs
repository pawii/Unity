using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

	public enum MouseAxis { X = 0, Y, YAndX };
	public MouseAxis mouseAxis = MouseAxis.X;
	public float speed = 7f;
	public float minVert = -45;
	public float maxVert = 45;

	private float rotateX;
	private float rotateY;

	// Use this for initialization
	void Start () {
		rotateX = transform.localEulerAngles.x;
		rotateY = transform.localEulerAngles.y;
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb != null)
			rb.freezeRotation = true;
	}

	// Update is called once per frame
	void Update () {
		if (mouseAxis == MouseAxis.X) {
			rotateY += Input.GetAxis ("Mouse X") * speed;
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, rotateY, 0);
		} else if (mouseAxis == MouseAxis.Y) {
			rotateX -= Input.GetAxis ("Mouse Y") * speed;
			rotateX = Mathf.Clamp (rotateX, minVert, maxVert);
			transform.localEulerAngles = new Vector3 (rotateX, transform.localEulerAngles.y, 0);
		} else {
			rotateY += Input.GetAxis ("Mouse X") * speed;
			rotateX -= Input.GetAxis ("Mouse Y") * speed;
			rotateX = Mathf.Clamp (rotateX, minVert, maxVert);
			transform.localEulerAngles = new Vector3 (rotateX, rotateY, 0);
		}
	}
}
