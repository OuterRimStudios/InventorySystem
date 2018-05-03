using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Slot inventorySlot;
}

[System.Serializable]
public class Slot
{
    public Image icon;
    public Text stackSizeText;

    [HideInInspector]
    public GameObject inventorySlotGO;
    [HideInInspector]
	public Item item;
    [HideInInspector]
    public int stackSize;

    public Slot(Item _item, int _stackSize)
    {
        item = _item;
        stackSize = _stackSize;
    }

	public void UpdateUI()
	{
        icon.sprite = item.uiIcon;
        stackSizeText.text = stackSize.ToString();
	}
}