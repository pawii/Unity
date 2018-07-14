using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour 
{
	[SerializeField]
	private Slider progressBar;

	void Awake()
	{
		Messenger<int, int>.AddListener(StartupEvent.MANAGER_PROGRESS, OnManagerProgress);
		Messenger.AddListener(StartupEvent.MANAGER_STARTED, OnManagerStarted);
	}

	void Destroy()
	{
		Messenger<int, int>.RemoveListener(StartupEvent.MANAGER_PROGRESS, OnManagerProgress);
		Messenger.RemoveListener(StartupEvent.MANAGER_STARTED, OnManagerStarted);
	}

	void OnManagerProgress(int numReady, int numModules)
	{
		float progress = (float)numReady / numModules;
		progressBar.value = progress;
	}

	void OnManagerStarted()
	{
		Managers.Mission.GoToNext();
	}
}