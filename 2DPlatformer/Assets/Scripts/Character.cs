using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit 
{
	[SerializeField]
	private float speed = 3f;
	[SerializeField]
	private int lives = 5;
	[SerializeField]
	private float jumpForse = 15f;

	private Rigidbody2D rigidbody;
	private Animator animator;
	private SpriteRenderer sprite;

	private bool isGrounded = true;

	private Bullet bullet;

	private LivesBar livesBar;

	public int Lives
	{
		get { return lives; }
		set
		{
			if (value <= 5) { lives = value; livesBar.Refresh(); }
		}
	}

	void Awake()
	{
		livesBar = FindObjectOfType<LivesBar>();		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();

		bullet = Resources.Load<Bullet>("Bullet");
	}

	void FixedUpdate()
	{
		CheckGround();
	}

	void Update()
	{
		if (isGrounded) animator.SetBool("isJump", false);
		else animator.SetBool("isJump", true);
		if (Input.GetButton("Horizontal")) { animator.SetFloat("speed", 1f); Run();  }
		else animator.SetFloat("speed", -1f);
		if (Input.GetButton("Jump") && isGrounded) Jump();
		if (Input.GetButtonDown("Fire1")) Shoot();
	}

	void Run()
	{
		Vector3 direction = transform.right * Input.GetAxis("Horizontal");
		transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

		sprite.flipX = direction.x < 0;
	}

	void Jump()
	{
		rigidbody.AddForce(transform.up * jumpForse, ForceMode2D.Impulse); 
	}

	void Shoot()
	{
		Vector3 position = transform.position;
		position.y += 1f;
		Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

		newBullet.Direction = transform.right * (sprite.flipX ? -1 : 1);
		newBullet.Parent = gameObject;
	}

	public override void ReceiveDamage()
	{
		Lives--;

		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(transform.up * 8f, ForceMode2D.Impulse);

		Debug.Log("Lives: " + lives);
	}

	void CheckGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

		isGrounded = colliders.Length > 1;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Bullet bullet = collider.gameObject.GetComponent<Bullet>();
		if (bullet && bullet.Parent != gameObject) ReceiveDamage();
	}
}
