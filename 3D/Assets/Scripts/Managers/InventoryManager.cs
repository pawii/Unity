using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }
	private NetworkService _network;

	public string equippedItem { get; private set;}

	private Dictionary<string, int> _items;

	public void StartUp(NetworkService network)
	{
		status = ManagerStatus.Initializing;
		Debug.Log("Iventory manager started...");

		_network = network;

		_items = new Dictionary<string, int>();
		status = ManagerStatus.Started;
	}

	private void DisplayItems()
	{
		string itemDisplay = "Items: ";

		foreach (KeyValuePair<string, int> item in _items)
			itemDisplay += item.Key + "(" + item.Value + ") ";

		Debug.Log(itemDisplay);
	}

	public void AddItem(string name)
	{
		if (_items.ContainsKey(name))
			_items[name]++;
		else
			_items[name] = 1;
		
		DisplayItems();
	}

	public List<string> GetItemList()
	{
		List<string> list = new List<string>(_items.Keys);
		return list;
	}

	public int GetItemCount(string name)
	{
		if (_items.ContainsKey(name))
			return _items[name];
		else
			return 0;
	}

	public bool EquipItem(string name)
	{
		if (_items.ContainsKey(name) && equippedItem != name)
		{
			equippedItem = name;
			Debug.Log("Equipped: " + name);
			return true;
		}

		equippedItem = null;
		Debug.Log("Unequipped");
		return false;
	}

	public bool ConsumeItem(string name)
	{
		if (_items.ContainsKey(name))
		{
			_items[name]--;
			if (_items[name] == 0)
				_items.Remove(name);
			DisplayItems();
			return true;
		}

		Debug.Log("Cannot consume " + name);
		return false;
	}
}
