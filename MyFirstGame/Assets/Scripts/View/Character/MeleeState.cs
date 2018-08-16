using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : MonoBehaviour, ICharacterState 
{
	GameObject meleePrefab;
	GameObject curObj;
	MeleeTorso meleeTorso;
	CharacterMovement context;
	ICharacterState anotherState;

	public bool fastSpeed { get; set; }
	public bool isRun 
	{
		get { return meleeTorso.IsRun; }
		set { meleeTorso.IsRun = value; }
	}

	public MeleeState(CharacterMovement context)
	{
		fastSpeed = true;

		meleePrefab = Resources.Load<GameObject>("MeleeWeapon");
		this.context = context;

        //Create();
	}

	public MeleeState(CharacterMovement context, ICharacterState anotherState) : this(context)
	{
		this.anotherState = anotherState;
	}

	public void OnKeyDown(KeyCode key)
	{
		if (key == KeyCode.Q)
		{
			Destroy();

			if (anotherState == null)
				anotherState = new ArchState(context, this);
			else
				anotherState.Create();

			context.CharacterState = anotherState;
		}
	}

	void Destroy()
	{
		if (curObj != null)
			Destroy(curObj);
	}

	public void Create()
	{
		curObj = Instantiate(meleePrefab) as GameObject;

		meleeTorso = curObj.GetComponent<MeleeTorso>();
		meleeTorso.mediator = this;
		curObj.GetComponentInChildren<Axe>().mediator = this;

		if (context.FlipX)
			curObj.transform.localScale = new Vector3(-5, 5, 1);
		else
			curObj.transform.localScale = new Vector3(5, 5, 1);
		curObj.transform.parent = context.transform.Find("TorsoDown");
		curObj.transform.localPosition = new Vector2(0, 0);

		isRun = false;
	}

	public bool GetFlipX()
	{
		return context.FlipX;
	}
}