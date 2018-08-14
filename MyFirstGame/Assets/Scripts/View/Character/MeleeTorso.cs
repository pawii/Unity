using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTorso : MonoBehaviour
{
	public MeleeState mediator { private get; set; }
	public bool IsRun 
	{
		get { return anim.GetBool("Run"); }
		set { anim.SetBool("Run", value); }
	}

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			anim.SetTrigger("Attack");
		
		if (Input.GetMouseButtonDown(1))
		{
			anim.SetBool("Protect", true);
			mediator.fastSpeed = false;
		}

		if (Input.GetMouseButtonUp(1))
		{
			anim.SetBool("Protect", false);
			mediator.fastSpeed = true;
		}

	}
}
