using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopulator : MonoBehaviour
{
    public Item[] items;
    public Inventory inventory;
    
	IEnumerator Start ()
    {
	    foreach(Item item in items)
        {
            inventory.AddItem(item);
        }

        yield return new WaitForSeconds(5f);

        foreach(Item item in items)
        {
            inventory.RemoveItem(item);
        }
	}
}
