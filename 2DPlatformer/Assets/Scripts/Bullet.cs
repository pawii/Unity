using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float speed = 10f;
	private GameObject parent;
	public GameObject Parent { get { return parent;} set { parent = value; } }
	public Color Color
	{
		set { sprite.color = value; }
	}

	private SpriteRenderer sprite;
	private Vector3 direction;

	public Vector3 Direction { set { direction = value; } }

	void Awake()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
	}

	void Start()
	{
		Destroy(gameObject, 1.5f);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Unit unit = collider.GetComponent<Unit>();

		if (unit && unit.gameObject != parent)
		{
			Destroy(gameObject); 
		}
	}
}
