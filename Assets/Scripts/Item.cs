using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Item")]
public class Item : ScriptableObject {

	public string itemName;
	public int stackSize;
	public int slotCount;
	public Sprite uiIcon;
	public GameObject model;
}