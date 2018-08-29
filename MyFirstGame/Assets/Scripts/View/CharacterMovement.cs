using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour 
{
	public float runSpeed = 5f;
	public float walkSpeed = 2.5f;
	public float jumpPower = 10f;
	[SerializeField]
	int getDamagePower = 5;

	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Run(bool isRun)
	{
		Vector2 direction = transform.right * Input.GetAxis("Horizontal");


		float speed = isRun ? runSpeed : walkSpeed;
		transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, 
		                                         speed * Time.deltaTime);
	}

	public void Jump()
	{
		Vector2 force = transform.up * jumpPower;
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void GetDamage(int direction)
	{
		Vector2 getDamageForce = new Vector2(0.1f, 1);
		getDamageForce.x *= direction;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower* getDamageForce, ForceMode2D.Impulse);
		}
	}
}