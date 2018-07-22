using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
	void Awake()
	{
		Messenger.AddListener(StartupEvent.MANAGER_STARTED, OnManagerStarted);
	}

	void Destroy()
	{
		Messenger.RemoveListener(StartupEvent.MANAGER_STARTED, OnManagerStarted);
	}

	void OnManagerStarted()
	{
		Managers.Mission.GoToNext();
	}
}