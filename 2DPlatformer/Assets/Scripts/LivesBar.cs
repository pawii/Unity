using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour 
{
	[SerializeField]
	private Transform[] hearts;
	[SerializeField]
	private Character character;

	public void Refresh()
	{
		for (int i = 0; i < hearts.Length; i++)
			if (i < character.Lives)
			{
				hearts[i].gameObject.SetActive(true);
			}
			else
				hearts[i].gameObject.SetActive(false);
	}
}
