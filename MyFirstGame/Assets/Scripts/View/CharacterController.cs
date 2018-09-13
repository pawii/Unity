using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : Unit
{
	public static bool flipX;
	private bool fastSpeed;

	public static event Action<bool> Run;
	public static event Action Jump;
	public static event Action<int> GetDamage;

	float radiusInteraction = 2f;

	public static bool Lock { get; set; }

	[SerializeField]
	private Animator anim;
	private int AnimatorState
	{
		get { return anim.GetInteger("state"); }
		set { anim.SetInteger("state", value); }
	}

	bool isGrounded;

	void Awake()
	{
		fastSpeed = true;

		ArchTorso.FastSpeedChanged += OnFastSpeedChanged;
		MeleeTorso.FastSpeedChanged += OnFastSpeedChanged;

		WeaponFactory.SetArch(transform, FlipX);

		FlipX = false;
		flipX = false;

		Lock = false;

		isGrounded = true;
	}

	void OnDestroy()
	{
		ArchTorso.FastSpeedChanged -= OnFastSpeedChanged;
		MeleeTorso.FastSpeedChanged -= OnFastSpeedChanged;
	}

	void Update()
	{
		// ВЫЧИСЛЕНИЕ FLIPX

		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;

		if (newRight.x < 0 && !FlipX)
		{
			FlipX = true;
			flipX = true;
		}

		else if (newRight.x >= 0 && FlipX)
		{
			FlipX = false;
			flipX = false;
		}


		// ДВИЖЕНИЕ И АНИМАЦИЯ

		if (!Lock)
		{
            SetGrounded();

			if (isGrounded) 
				AnimatorState = (int)AnimationState.Idle;
			
			if (Input.GetButton("Horizontal"))
			{
				bool isRun = fastSpeed || !isGrounded;
				Run(isRun);

				if (isGrounded)
					//if (!FlipX)
						//if (fastSpeed)
							AnimatorState = (int)AnimationState.Run;
						//else
						//	AnimatorState = (int)AnimationState.Walk;
				//	else
					//	if (fastSpeed)
						//	AnimatorState = (int)AnimationState.RunBack;
						//else
						//	AnimatorState = (int)AnimationState.WalkBack;
			}
			if (Input.GetButtonDown("Jump") && isGrounded) 
			{
				Jump(); 
				AnimatorState = (int)AnimationState.Jump;
			}

			if (!isGrounded)
				AnimatorState = (int)AnimationState.Jump;
		}
	}


	// РЕАКЦИЯ НА НАЖАТИЕ КЛАВИШ
	// (ЕСЛИ БУДЕТ МНОГО КОДА, МОЖНО СДЕЛАТЬ ЦЕПОЧКУ ОБЯЗАННОСТЕЙ)

	void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey)
		{
			switch (e.keyCode)
			{
				case KeyCode.Q:
					WeaponFactory.SetArch(transform, FlipX);
					break;

				case KeyCode.E:
					WeaponFactory.SetMelee(transform, FlipX);
					break;

				case KeyCode.W:
					Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusInteraction);
					for (int i = 0; i < colliders.Length; i++)
						colliders[i].gameObject.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
					break;
			}
		}
	}

	public void SetGrounded()
	{
		isGrounded = false;

		Vector3 pos = transform.position;
		pos.y -= 1f;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 0.1f);
		for (int i = 0; i < colliders.Length; i++)
			if (colliders[i].gameObject.layer != LayerMask.NameToLayer("dont hit"))
				isGrounded = true;	}

	public void OnHit(MessageParameters parameters)
	{
		GameController.ChangeHealth(parameters.Damage);

		GetDamage(parameters.Direction);	}

	private void OnFastSpeedChanged(bool fastSpeed)
	{
		this.fastSpeed = fastSpeed;
	}
}