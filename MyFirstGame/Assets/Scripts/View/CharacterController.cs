using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : Unit
{
	WeaponObserver weaponObserver;
	CharacterMovement movement;

	float radiusInteraction = 2f;

	public static bool Lock { get; set; }

	Animator anim;
	private int AnimatorState
	{
		get { return anim.GetInteger("state"); }
		set { anim.SetInteger("state", value); }
	}

	bool isGrounded;

	void Awake()
	{
		weaponObserver = GetComponent<WeaponObserver>();
		movement = GetComponent<CharacterMovement>();
		weaponObserver.SetArch();

		FlipX = false;

		Lock = false;

		anim = GetComponent<Animator>();

		isGrounded = true;
	}

	void Update()
	{
		// ВЫЧИСЛЕНИЕ FLIPX

		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 newRight = mousePos - (Vector2)transform.position;
		if (newRight.x< 0 && !FlipX)
			FlipX = true;
		else if (newRight.x >= 0 && FlipX)
			FlipX = false;


		// ДВИЖЕНИЕ И АНИМАЦИЯ

		if (!Lock)
		{
            SetGrounded();
			if (isGrounded) { AnimatorState = (int)AnimationState.Idle; weaponObserver.isRun = false; }
			if (Input.GetButton("Horizontal"))
			{
				bool isRun = weaponObserver.fastSpeed || !isGrounded;
				movement.Run(isRun);

				if (isGrounded)
					if (!FlipX)
						if (weaponObserver.fastSpeed)
							AnimatorState = (int)AnimationState.Run;
						else
							AnimatorState = (int)AnimationState.Walk;
					else
						if (weaponObserver.fastSpeed)
							AnimatorState = (int)AnimationState.RunBack;
						else
							AnimatorState = (int)AnimationState.WalkBack;
				
				weaponObserver.isRun = true;
			}
			if (Input.GetButtonDown("Jump") && isGrounded) { movement.Jump(); AnimatorState = (int)AnimationState.Jump;}
			if (!isGrounded) { AnimatorState = (int)AnimationState.Jump; weaponObserver.isRun = false; }
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
					weaponObserver.SetArch();
					break;
				case KeyCode.E:
					weaponObserver.SetMelee();
					break;
				case KeyCode.W:
					Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusInteraction);
					foreach (Collider2D collider in colliders)
						collider.gameObject.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
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
		foreach (Collider2D collider in colliders)
			if (collider.gameObject.layer != LayerMask.NameToLayer("dont hit"))
				isGrounded = true;	}

	public void OnHit(MessageParameters parameters)
	{
		GameController.ChangeHealth(parameters.Damage);

		movement.GetDamage(parameters.Direction);	}
}