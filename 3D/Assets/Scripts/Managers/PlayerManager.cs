using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }
	private NetworkService _network;

	public int health { get; private set; }
	public int maxHealth { get; private set; }

	public void StartUp(NetworkService network)
	{		status = ManagerStatus.Initializing;

		_network = network;

		UpdateData(50, 100);

		status = ManagerStatus.Started;
	}

	public void ChangeHealth(int value)
	{
		health += value;
		if (health > maxHealth)
			health = maxHealth;
		else if (health < 0)
			health = 0;

		if (health == 0)
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
	}

	public void UpdateData(int health, int maxHealth)
	{
		this.health = health;
		this.maxHealth = maxHealth;
	}

	public void Respawn()
	{
		UpdateData(50, 100);
	}
}
