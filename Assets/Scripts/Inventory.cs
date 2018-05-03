using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public InventorySlot inventorySlotPrefab;
	public RectTransform inventoryContentArea;
	public int inventorySlotCountMax;
	int inventorySlotsUsed;																			//Current amount of slots
	public bool canDropItems;
	[Tooltip("Each instance of an item added to the inventory consumes slots")]
	public bool stacksConsumeSlots;

    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<InventorySlot> occupiedInventorySlots;

	void Awake()
	{
        UpdateUI();
	}

	public void AddItem(Item itemToAdd)
	{
        print("InventoryContains(itemToAdd) " + InventoryContains(itemToAdd));
        if (InventoryContains(itemToAdd))															//Check if our inventory contains the item we're adding
		{
			int slotIndex = GetSlotIndex(itemToAdd);                                                //Get the index of the InventorySlot in the occupiedInventorySlots List that holds the added item

            print("slotIndex >= 0 " + (slotIndex >= 0));
            if (slotIndex >= 0)
			{
                print("occupiedInventorySlots[slotIndex].stackSize == " + occupiedInventorySlots[slotIndex].stackSize + "itemToAdd.stackSize == " + itemToAdd.stackSize);
				if(occupiedInventorySlots[slotIndex].stackSize < itemToAdd.stackSize)				//Check if our InventorySlot is at max stack size
				{
                    print("stacksConsumeSlots " + stacksConsumeSlots);
					if(stacksConsumeSlots)															//Check if each instance of an item in a stack consumes slots
					{
                        print("SlotsAvailable(itemToAdd.slotCount) " + SlotsAvailable(itemToAdd.slotCount));
						if(SlotsAvailable(itemToAdd.slotCount))										//Check if we have inventory slots available
						{
							inventorySlotsUsed += itemToAdd.slotCount;								//Add the amount of slots used by the item
							occupiedInventorySlots[slotIndex].stackSize++;							//Increase stack size
						}
						else
							Debug.LogError("Inventory is Full");
					}
					else
						occupiedInventorySlots[slotIndex].stackSize++;								//Increase stack size
				}
				else
                {
                    print("The inventory contains " + itemToAdd.name);
                    if (SlotsAvailable(itemToAdd.slotCount))											//Check if we have inventory slots available
					{
						inventorySlotsUsed += itemToAdd.slotCount;									//Add the amount of slots used by the item
						CreateInventorySlot(itemToAdd);												//Create a new InventorySlot for the item picked up
					}
					else
						Debug.LogError("Inventory is Full");
				}
			}
			else
				Debug.LogError("GetSlotIndex returned: " + slotIndex);
		}
		else
		{
			if(SlotsAvailable(itemToAdd.slotCount))													//Check if we have inventory slots available
			{
				inventorySlotsUsed += itemToAdd.slotCount;											//Add the amount of slots used by the item
				CreateInventorySlot(itemToAdd);														//Create a new InventorySlot for the item picked up
			}
			else
				Debug.LogError("Inventory is Full");
		}

        UpdateUI();
    }

	void CreateInventorySlot(Item slotItem)
	{
        if (occupiedInventorySlots.Capacity == 0)
            occupiedInventorySlots = new List<InventorySlot>();

        InventorySlot newSlot = new InventorySlot(slotItem, 1);

        print("New SLOOTT "+  newSlot);
		occupiedInventorySlots.Add(newSlot);

        foreach(InventorySlot slot in occupiedInventorySlots)
        {
            print("SLOOTT " + slot);
        }
    }

	bool SlotsAvailable(int slotsToUse)
	{
		return inventorySlotsUsed + slotsToUse < inventorySlotCountMax;
	}

	bool InventoryContains(Item itemToCheck)
	{
		for(int i = 0; i < occupiedInventorySlots.Count; i++)
		{
			if(occupiedInventorySlots[i].item.itemName == itemToCheck.itemName)
				return true;
		}

		return false;
	}

	int GetSlotIndex(Item itemToCheck)
	{
		for(int i = 0; i < occupiedInventorySlots.Count; i++)
		{
			if(occupiedInventorySlots[i].item.itemName == itemToCheck.itemName)
				return i;
		}

		return -1;
	}

	void UpdateUI()
	{
        for(int k = 0; k < inventorySlots.Count; k++)                                   //Remove unoccupied invetory slots
        {
            if(!occupiedInventorySlots.Contains(inventorySlots[k].GetComponent<InventorySlot>()))
            {
                GameObject tempSlot = inventorySlots[k];
                inventorySlots.Remove(inventorySlots[k]);
                Destroy(tempSlot);
            }
        }

        for(int i = 0; i < occupiedInventorySlots.Count; i++)                           //Add the occupied slots to your inventory slots
        {
            if(occupiedInventorySlots[i].gameObject == null)
            {
                InventorySlot newSlot = Instantiate(inventorySlotPrefab, inventoryContentArea);

                newSlot.item = occupiedInventorySlots[i].item;
                newSlot.stackSize = occupiedInventorySlots[i].stackSize;
                newSlot.UpdateUI();

                occupiedInventorySlots[i] = newSlot;
                inventorySlots.Add(newSlot.gameObject);
            }
            else if(!inventorySlots.Contains(occupiedInventorySlots[i].gameObject))
            {
                print("Inventory does not contain this item");
                InventorySlot newSlot = Instantiate(inventorySlotPrefab, inventoryContentArea);

                newSlot.item = occupiedInventorySlots[i].item;
                newSlot.stackSize = occupiedInventorySlots[i].stackSize;
                newSlot.UpdateUI();

                occupiedInventorySlots[i] = newSlot;
                inventorySlots.Add(newSlot.gameObject);
            }
        }

        int unUsedSlots = inventorySlotCountMax - inventorySlotsUsed;

        for (int j = 0; j < unUsedSlots; j++)                                           //Add the empty slots back to the invetory
        {
            if(inventorySlotPrefab)
            {
                InventorySlot newSlot = Instantiate(inventorySlotPrefab, inventoryContentArea);
                inventorySlots.Add(newSlot.gameObject);
            }
        }
    }
}