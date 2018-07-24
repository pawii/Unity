using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public GameObject Parent { get; set; }
	private Rigidbody2D rb;
	private bool hit = true;


	private Vector2 force;
	Vector2 startPos;
	Vector2 endPos;
	Vector2 startRot;
	Vector2 attachOffset;
	Transform target;


	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!hit)
		{
			float percent = (transform.position.x - startPos.x) / ((endPos.x - startPos.x) / 2);
			Vector2 curRot;
			curRot.x = startRot.x;
			curRot.y = startRot.y - startRot.y * percent;
			transform.right = curRot;
		}
		else
		{
			if (target)				transform.position = (Vector2)target.position - attachOffset;
			else
				Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Vector2 spherePos = transform.position + transform.right / 10;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(spherePos, 0.1f);
		if (colliders.Length > 0)
		{
			if (collider.gameObject != Parent && !hit && collider.gameObject.tag != "arrow"
			    && collider.gameObject.tag != "bow")
			{
				attachOffset = collider.transform.position - transform.position;
				target = collider.gameObject.transform;
				collider.gameObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
				StartCoroutine(Hitting());
			}
		}
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
		return angel;	}

	public void Shoot(Vector2 force)
	{
		this.force = force;
		CalculateData();
		rb.AddForce(force, ForceMode2D.Impulse);
		hit = false;
	}
}