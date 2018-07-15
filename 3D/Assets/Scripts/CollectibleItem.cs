using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

	[SerializeField]
	string itemName;

	void OnTriggerEnter(Collider other)
	{
		PlayerCharacter pc = other.GetComponent<PlayerCharacter>();
		if (pc != null)
		{
			Managers.Inventory.AddItem(itemName);
			Destroy(this.gameObject);
		}
	}
}
