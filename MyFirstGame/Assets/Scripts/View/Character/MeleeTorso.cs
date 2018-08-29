using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTorso : MonoBehaviour
{
	Axe axe;
	WeaponObserver observer;
	public WeaponObserver Observer 
	{ 
		private get { return observer; } 
		set 
		{
			observer = value;
			axe.Observer = value;
		}
	}
	public bool IsRun 
	{
		get { return anim.GetBool("Run"); }
		set { anim.SetBool("Run", value); }
	}

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
		axe = GetComponentInChildren<Axe>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			anim.SetTrigger("Attack");
		
		if (Input.GetMouseButtonDown(1))
		{
			anim.SetBool("Protect", true);
			Observer.fastSpeed = false;
		}

		if (Input.GetMouseButtonUp(1))
		{
			anim.SetBool("Protect", false);
			Observer.fastSpeed = true;
		}

	}
}
