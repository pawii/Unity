using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Monster 
{
	float minCoordY;
	[SerializeField]
	private Animator anim;
	[SerializeField] 
	private Transform bow;

	void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 7f;
		damageRate = 2f;

		triggerArea = 7;

		character = GameController.character;

		velocity = (float)Mathf.Sqrt((float)damageArea* (float)Physics2D.gravity.magnitude);
		minCoordY = -0.5f;

		movement = new TwoPointMovement(FlipX, transform, xMinPos, xMaxPos);
		movement.ChangeFlipX += OnChangeFlipX;

		getDamagePower = 5;	}

	void OnDestroy()
	{
		movement.ChangeFlipX -= OnChangeFlipX;
	}

	protected override void SetCalm()
	{
		base.SetCalm();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new TwoPointMovement(FlipX, transform, xMinPos, xMaxPos);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetTriggered()
	{
		base.SetTriggered();
		anim.SetBool("attack", false);
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new AgressiveMovement(FlipX, transform, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetAgressive()
	{
		base.SetAgressive();
		anim.SetBool("attack", true);
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new StayInPlaceMovement(FlipX, transform.position, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void Attack()
	{
		Vector2 force = FlipX ? bow.right : -bow.right;
		force *= velocity;
		BulletFactory.CreateArrow(bow, damage, force, tag);
	}
}
