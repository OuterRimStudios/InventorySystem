using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Item")]
public class Item : ScriptableObject 
{
	public EquippableSlotType equippableSlotTypeSO;
	[HideInInspector] public string itemName;
	[HideInInspector] public int stackSize;
	[HideInInspector] public int slotCount;
	[HideInInspector] public Sprite uiIcon;
	[HideInInspector] public GameObject model;
	[HideInInspector] public bool equippable;	
	[HideInInspector] public string equippableSlotType;
	[HideInInspector] public int equippableSlotTypeIndex;
}