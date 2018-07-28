using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }
	public bool ligth { get; set; }

	public void StartUp()
	{
		status = ManagerStatus.Initializing;
		Debug.Log("Inventory manager starting...");

		ligth = false;

		status = ManagerStatus.Started;
	}
}