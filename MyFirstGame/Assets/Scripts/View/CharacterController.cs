using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
	private static bool flipX;
    public static bool Get_FlipX() { return flipX; }
    private void Set_FlipX(bool value)
    {
        if (value == flipX)
            return;
        else
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            flipX = !flipX;
        }
    }

	private bool fastSpeed;

	public static event Action<bool> Run;
	public static event Action Jump;
	public static event Action<int> GetDamage;

	private float radiusInteraction = 2f;

	public static bool Lock { get; private set; }

	[SerializeField]
	private Animator anim;
	private int AnimatorState
	{
		get { return anim.GetInteger("state"); }
		set { anim.SetInteger("state", value); }
	}

	private bool isGrounded;

    #region Unity lifecycle
    private void Awake()
	{
		fastSpeed = true;

		ArchTorso.FastSpeedChanged += OnFastSpeedChanged;
		MeleeTorso.FastSpeedChanged += OnFastSpeedChanged;

        flipX = false;

		Lock = false;

		isGrounded = true;

        Messenger.AddListener(GameEvent.CHARACTER_HIDED, OnCharacterHided);
        Messenger.AddListener(GameEvent.CHARACTER_SEEMED, OnCharacterSeemed);

        WeaponFactory.SetArch(transform, flipX);
    }

	void OnDestroy()
	{
		ArchTorso.FastSpeedChanged -= OnFastSpeedChanged;
		MeleeTorso.FastSpeedChanged -= OnFastSpeedChanged;

        Messenger.RemoveListener(GameEvent.CHARACTER_HIDED, OnCharacterHided);
        Messenger.RemoveListener(GameEvent.CHARACTER_SEEMED, OnCharacterSeemed);
    }

	void Update()
	{
		// ВЫЧИСЛЕНИЕ FLIPX

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 newRight = mousePos - transform.position;

        if (newRight.x < 0 && !flipX)
            Set_FlipX(true);

        else if (newRight.x >= 0 && flipX)
            Set_FlipX(false);


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
			    //  if (!FlipX)
				//      if (fastSpeed)
							AnimatorState = (int)AnimationState.Run;
				//      else
				//      	AnimatorState = (int)AnimationState.Walk;
				//  else
				//      if (fastSpeed)
				//      	AnimatorState = (int)AnimationState.RunBack;
				//      else
				//	        AnimatorState = (int)AnimationState.WalkBack;
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
					WeaponFactory.SetArch(transform, flipX);
					break;

				case KeyCode.E:
					WeaponFactory.SetMelee(transform, flipX);
					break;

				case KeyCode.W:
					Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusInteraction);
                    for (int i = 0; i < colliders.Length; i++)
                        colliders[i].gameObject.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
					break;
			}
		}
	}
    #endregion

    private void OnCharacterSeemed()
    {
        Lock = false;
    }

    private void OnCharacterHided()
    {
        Lock = true;
    }

    public void SetGrounded()
	{
		isGrounded = false;

		Vector3 pos = transform.position;
		pos.y -= 1f;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 0.1f);
		for (int i = 0; i < colliders.Length; i++)
			if (colliders[i].gameObject.layer != LayerMask.NameToLayer("dont hit"))
				isGrounded = true;
	}

	public void OnHit(MessageParameters parameters)
	{
		GameController.ChangeHealth(parameters.damage);

		GetDamage(parameters.direction);
	}

	private void OnFastSpeedChanged(bool fastSpeed)
	{
		this.fastSpeed = fastSpeed;
	}
}