using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	private GameObject parent;
	public GameObject Parent { set { parent = value; } }
	public float rotSpeed = 1.5f;
	private Rigidbody2D rb;
	private bool hit = true;


	private Vector2 force;
	Vector2 startPos;
	Vector2 endPos;
	Vector2 startRot;
	Vector2 curRot;
	float percent;



	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!hit)
		{
			percent = (transform.position.x - startPos.x) / ((endPos.x - startPos.x) / 2);
			curRot.x = startRot.x;
			curRot.y = startRot.y - startRot.y * percent;
			transform.right = curRot;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject != parent)
			StartCoroutine(Hitting());
	}

	IEnumerator Hitting()
	{
		hit = true;
		rb.bodyType = RigidbodyType2D.Static;
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	void CalculateData()
	{
		startRot = transform.right;
		startPos = transform.position;
		float time = (2f * (float)force.magnitude * (float)Mathf.Sin((float)Angle(new Vector2(1, 0), force))) / (float)Physics2D.gravity.magnitude;
		endPos = startPos;
		endPos.x += Vector2.Dot(force, new Vector2(1, 0)) * time;
	}

	private float Angle(Vector2 start, Vector2 end)
	{
		float angel = Vector2.Angle(start, end);
		angel = angel / 180 * Mathf.PI;
		return angel;	 }

	public void Shoot(Vector2 force)
	{
		this.force = force;
		CalculateData();
		rb.AddForce(force, ForceMode2D.Impulse);
		hit = false;
	}
}