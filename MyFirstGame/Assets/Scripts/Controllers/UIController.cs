using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{
	[SerializeField]
	private Text notificationText;
	public bool NotificationActive
	{
		private get { return notificationText.gameObject.activeSelf; }
		set { notificationText.gameObject.SetActive(value); }
	}
	public string NotificationText
	{
		private get { return notificationText.text; }
		set { notificationText.text = value; }	}

	void Awake()
	{
		NotificationActive = false;
	}
}
