using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour 
{
	Animator anim;
	private int State
	{
		get { return anim.GetInteger("state"); }
		set { anim.SetInteger("state", value); }	}

	public float speed = 5f;
	public float jumpPower = 10f;
	[SerializeField]
	int getDamagePower = 5;

	private Rigidbody2D rb;

	private bool isGrounded = true;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (!CharacterController.Lock)
		{
			SetGrounded();
			if(isGrounded) State = (int)AnimationState.Idle;
			if (Input.GetButton("Horizontal")) Run();
			if (Input.GetButtonDown("Jump") && isGrounded) Jump();
			if (!isGrounded) State = (int)AnimationState.Jump;
		}
	}

	void SetGrounded()
	{
		isGrounded = false;

		Vector3 pos = transform.position;
		pos.y -= 0.8f;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 0.1f);
		foreach (Collider2D collider in colliders)
			if (collider.gameObject.layer == LayerMask.NameToLayer("ground"))
				isGrounded = true;
	}

	void Run()
	{
		Vector2 direction = transform.right * Input.GetAxis("Horizontal");

		if (isGrounded)
			if (direction.x * transform.localScale.x > 0)
				State = (int)AnimationState.Run;
			else
				State = (int)AnimationState.RunBack;


		transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, 
		                                         speed * Time.deltaTime);
	}

	void Jump()
	{
		Vector2 force = transform.up * jumpPower;
		rb.AddForce(force, ForceMode2D.Impulse);

		State = (int)AnimationState.Jump;
	}

	public void OnHit(MessageParameters parameters)
	{
		GameController.ChangeHealth(parameters.Damage);

		Vector2 getDamageForce = new Vector2(0.1f, 1);
		int getDamageDiretion = parameters.Sprite.flipX ? 1 : -1;
		getDamageForce.x *= getDamageDiretion;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
		}
	}
}
