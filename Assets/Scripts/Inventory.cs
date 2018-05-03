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
    public List<Slot> occupiedInventorySlots = new List<Slot>();

	void Awake()
	{
        UpdateUI();
	}

	public void AddItem(Item itemToAdd)
	{
        if (InventoryContains(itemToAdd))															//Check if our inventory contains the item we're adding
		{
			int slotIndex = GetSlotIndex(itemToAdd);                                                //Get the index of the InventorySlot in the occupiedInventorySlots List that holds the added item

            if (slotIndex >= 0)
			{
				if(occupiedInventorySlots[slotIndex].stackSize < itemToAdd.stackSize)				//Check if our InventorySlot is at max stack size
				{
					if(stacksConsumeSlots)															//Check if each instance of an item in a stack consumes slots
					{
						if(SlotsAvailable(itemToAdd.slotCount))										//Check if we have inventory slots available
						{
							inventorySlotsUsed += itemToAdd.slotCount;								//Add the amount of slots used by the item
							occupiedInventorySlots[slotIndex].stackSize++;							//Increase stack size
						}
						else
							Debug.LogError("Inventory is Full");
					}
					else
					{
						occupiedInventorySlots[slotIndex].stackSize++;								//Increase stack size
					}
				}
				else
                {
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
        Slot newSlot = new Slot(slotItem, 1);
		occupiedInventorySlots.Add(newSlot);
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
        if(inventorySlots.Count > 0)
		{
			for(int i = 0; i < inventorySlots.Count; i++)
			{
				Destroy(inventorySlots[i]);
			}

			inventorySlots.Clear();
		}

		if(occupiedInventorySlots.Count > 0)
		{
			for(int j = 0; j < occupiedInventorySlots.Count; j++)
			{
				InventorySlot newSlot = Instantiate(inventorySlotPrefab, inventoryContentArea);

				newSlot.inventorySlot.item = occupiedInventorySlots[j].item;
				newSlot.inventorySlot.stackSize = occupiedInventorySlots[j].stackSize;
				newSlot.inventorySlot.inventorySlotGO = newSlot.gameObject;
				newSlot.inventorySlot.UpdateUI();

				inventorySlots.Add(newSlot.gameObject);
			}
		}

		int unUsedSlots = inventorySlotCountMax - inventorySlotsUsed;

		for(int k = 0; k < unUsedSlots; k++)
		{
			InventorySlot newSlot = Instantiate(inventorySlotPrefab, inventoryContentArea);
			inventorySlots.Add(newSlot.gameObject);
		}
    }
}