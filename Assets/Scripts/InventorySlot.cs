using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text stackSizeText;

    [HideInInspector]
	public Item item;
    [HideInInspector]
    public int stackSize;

    public InventorySlot(Item _item, int _stackSize)
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