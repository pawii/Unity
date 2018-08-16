using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour 
{
	public ICharacterState CharacterState { get; set; }

	Animator anim;
	private int AnimatorState
	{
		get { return anim.GetInteger("state"); }
		set { anim.SetInteger("state", value); }	}

	public bool FlipX { get { return transform.localScale.x < 0; } }

	public float runSpeed = 5f;
	public float walkSpeed = 2.5f;
	public float jumpPower = 10f;
	[SerializeField]
	int getDamagePower = 5;

	private Rigidbody2D rb;

	private bool isGrounded = true;

	void Awake()
	{
		CharacterState = new ArchState(this);

		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (!CharacterController.Lock)
		{
			SetGrounded();
			if (isGrounded) { AnimatorState = (int)AnimationState.Idle; CharacterState.isRun = false; }
			if (Input.GetButton("Horizontal")) Run();
			if (Input.GetButtonDown("Jump") && isGrounded) Jump();
			if (!isGrounded) { AnimatorState = (int)AnimationState.Jump; CharacterState.isRun = false; }
		}
	}

	void OnGUI()
	{
		if (Event.current.isKey) CharacterState.OnKeyDown(Event.current.keyCode);
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
				if (CharacterState.fastSpeed)
					AnimatorState = (int)AnimationState.Run;
				else
					AnimatorState = (int)AnimationState.Walk;
			else
				if (CharacterState.fastSpeed)
					AnimatorState = (int)AnimationState.RunBack;
				else
					AnimatorState = (int)AnimationState.WalkBack;
		CharacterState.isRun = true;

		float speed = CharacterState.fastSpeed || !isGrounded ? runSpeed : walkSpeed;
		transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, 
		                                         speed * Time.deltaTime);
	}

	void Jump()
	{
		Vector2 force = transform.up * jumpPower;
		rb.AddForce(force, ForceMode2D.Impulse);

		AnimatorState = (int)AnimationState.Jump;
	}

	public void OnHit(MessageParameters parameters)
	{
		GameController.ChangeHealth(parameters.Damage);

		Vector2 getDamageForce = new Vector2(0.1f, 1);
		getDamageForce.x *= parameters.Direction;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
		}
	}
}
