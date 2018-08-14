using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchState : MonoBehaviour, ICharacterState
{
	GameObject archPrefab;
	GameObject curObj;
	ArchTorso archTorso;
	CharacterMovement context;
	ICharacterState anotherState;

	public bool fastSpeed { get; set; }
	public bool isRun { get; set; }

	public ArchState(CharacterMovement context)
	{
		fastSpeed = true;
		isRun = false;

		archPrefab = Resources.Load<GameObject>("ArchWeapon");
		this.context = context;

		Create();
	}

	public ArchState(CharacterMovement context, ICharacterState anotherState) : this(context)
	{
		this.anotherState = anotherState;
	}

	public void OnKeyDown(KeyCode key)
	{
		if (key == KeyCode.E)
		{
			Destroy();

			if (anotherState == null)
				anotherState = new MeleeState(context, this);
			else
				anotherState.Create();
			
			context.CharacterState = anotherState;
		}
	}

	void Destroy()
	{
		if(curObj != null)
			Destroy(curObj);
	}

	public void Create()
	{
		curObj = Instantiate(archPrefab) as GameObject;

		if (context.FlipX)
			curObj.transform.localScale = new Vector3(-5, 5, 1);
		else
			curObj.transform.localScale = new Vector3(5, 5, 1);
		curObj.transform.parent = context.transform.Find("TorsoDown");
		curObj.transform.localPosition = new Vector2(0, 0);

		archTorso = curObj.GetComponent<ArchTorso>();
		archTorso.Mediator = this;
	}

	public bool GetFlipX()
	{
		return context.FlipX;
	}
}