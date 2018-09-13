﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }
	public bool ligth { get; set; }

	#region Unity lifecycle
	public void StartUp()
	{
		status = ManagerStatus.Initializing;

		ligth = false;

		status = ManagerStatus.Started;
	}
	#endregion
}