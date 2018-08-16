using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public List<GameObject> parents;
	private Rigidbody2D rb;
	private bool hit = true;
	Transform colliderTransform;

	private Vector2 force;
	Vector2 startPos;
	Vector2 endPos;
	Vector2 startRot;
	Vector2 attachOffset;
	Transform target;
	int rotDirection;

	int damage;


	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		parents = new List<GameObject>();
		colliderTransform = transform.Find("Collider");
	}

	void Update()
	{
		if (!hit)
		{
			//SearchTrigger();

			float percent = (transform.position.x - startPos.x) / ((endPos.x - startPos.x) / 2);
			Vector2 curRot;
			curRot.x = startRot.x;
			if (rotDirection == 1)
				curRot.y = startRot.y - startRot.y * percent;
			else
				curRot.y = startRot.y + startRot.y* percent;
			transform.right = curRot;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		foreach (GameObject parent in parents)
			if (parent == collider.gameObject)
				return;

		if (!hit && collider.gameObject.layer != LayerMask.NameToLayer("dont hit"))
		{
			hit = true;
			Destroy(rb);

			transform.parent = collider.transform;

			int direction = force.x < 0 ? -1 : 1;
			MessageParameters parameters = new MessageParameters(direction, damage);
			if (collider.gameObject.tag != "shield")
			{
				collider.gameObject.SendMessageUpwards("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
				Debug.Log(collider.gameObject);
			}
			StartCoroutine(Hitting());
		}
	}

	void SearchTrigger()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(colliderTransform.position, 0.1f);
		foreach (Collider2D collider in colliders)
		{
			bool next = false;
			foreach (GameObject parent in parents)
				if (parent == collider.gameObject)
			{ next = true; break; }
			if (next)
				continue;

			if (!hit && collider.gameObject.layer != LayerMask.NameToLayer("dont hit"))
			{
				hit = true;
				Destroy(rb);

				transform.parent = collider.transform;

				int direction = force.x < 0 ? -1 : 1;
				MessageParameters parameters = new MessageParameters(direction, damage);
				if (collider.tag == null || collider.tag != "shield")
				{
					collider.gameObject.SendMessageUpwards("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
					Debug.Log(collider.gameObject);
				}
				StartCoroutine(Hitting());
			}
		}
	}

	IEnumerator Hitting()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	void CalculateData()
	{
		startRot = transform.right;
		startPos = transform.position;
		float time = (2f * (float)force.magnitude * (float)Mathf.Sin((float)Methods.Angle(new Vector2(1, 0), force))) / (float)Physics2D.gravity.magnitude;

		// БАГ - FORCE.Y = 0
		//Debug.Log(force.ToString());
		if (time == 0f)
			time++;
		// БАГ - FORCE.Y = 0

		endPos = startPos;
		endPos.x += Vector2.Dot(force, new Vector2(1, 0)) * time;
		rotDirection = transform.right.y < 0 ? -1 : 1;
	}

	public void Shoot(int damage, Vector2 force)
	{
		this.damage = damage;
		this.force = force;
		CalculateData();
		rb.AddForce(force, ForceMode2D.Impulse);
		hit = false;
	}
}