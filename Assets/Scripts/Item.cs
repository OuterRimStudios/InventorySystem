using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Item")]
public class Item : ScriptableObject {

	[SerializeField]
	string itemName;

	[SerializeField]
	Sprite uiIcon;

	public List<object> Addons {get; protected set;}
	[SerializeField]
	int[] selectionIndicies;

	public void AddAddon()
	{
		if(Addons == null)
			Addons = new List<object>();

		object addon = new object();
		Addons.Add(addon);
	}

	public void RemoveAddon(int index)
	{
		Addons.RemoveAt(index);
	}
}