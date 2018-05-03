using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopulator : MonoBehaviour
{
    public Item[] items;
    public Inventory inventory;
    
	void Start ()
    {
	    foreach(Item item in items)
        {
            inventory.AddItem(item);
        }
	}
}
