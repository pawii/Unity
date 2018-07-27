using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationText : MonoBehaviour 
{
	private Text text;

	public bool Active
	{
		private get { return gameObject.activeSelf; }
		set { gameObject.SetActive(value); }
	}
	public string Text
	{
		private get { return text.text; }
		set { text.text = value; }
	}

	void Awake()
	{
		text = GetComponent<Text>();
	}
}
