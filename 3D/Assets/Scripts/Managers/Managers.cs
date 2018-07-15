﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(AudioManager))]
public class Managers : MonoBehaviour 
{
	[SerializeField]
	private GameObject[] dontDestroy;

	public static InventoryManager Inventory { get; private set;}
	public static PlayerManager Player { get; private set; }
	public static MissionManager Mission { get; private set; }
	public static DataManager Data { get; private set; }
	public static AudioManager Audio { get; private set; }

	private List<IGameManager> _startSequence;


	void Awake () 
	{
		DontDestroyOnLoad(gameObject);
		foreach(GameObject gObject in dontDestroy)
			DontDestroyOnLoad(gObject);

		Inventory = GetComponent<InventoryManager>();
		Player = GetComponent<PlayerManager>();
		Mission = GetComponent<MissionManager>();
		Data = GetComponent<DataManager>();
		Audio = GetComponent<AudioManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);
		_startSequence.Add(Mission);
		_startSequence.Add(Data);
		_startSequence.Add(Audio);
		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers()
	{
		NetworkService network = new NetworkService();

		foreach (IGameManager manager in _startSequence)
			manager.StartUp(network);

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while (numReady < numModules)
		{
			int lastReady = numReady;
			numReady = 0;

			foreach (IGameManager manager in _startSequence)
				if (manager.status == ManagerStatus.Started)
					numReady++;

			if (numReady > lastReady)
			{
				Debug.Log("Progress: " + numReady + "/" + numModules);
				Messenger<int, int>.Broadcast(StartupEvent.MANAGER_PROGRESS, numReady, numModules);
			}

			yield return null;
		}

		Debug.Log("All managers started up");
		Messenger.Broadcast(StartupEvent.MANAGER_STARTED);
	}
}
