using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{
	public static LivesPanel LivesPanel { get; private set; }
	public static NotificationText NotificationText { get; private set; }

	void Awake()
	{
		LivesPanel = GetComponentInChildren<LivesPanel>();
		NotificationText = GetComponentInChildren<NotificationText>();
	}

	void Start()
	{
		NotificationText.Active = false;
	}
}
