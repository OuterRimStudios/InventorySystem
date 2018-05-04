using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippableCollection : MonoBehaviour
{
    public Inventory inventory;
    public CharacterCustomization characterCustomization;
    public List<EquippableSlot> equippableSlots;

    public void AddItem(Item itemToAdd)
    {
        for(int i = 0; i < equippableSlots.Count; i++)
        {
            if(itemToAdd.equippableSlotType == equippableSlots[i].equippableSlotType)
            {
                inventory.RemoveItem(itemToAdd);
                equippableSlots[i].UpdateItem(itemToAdd);
                characterCustomization.EquipItem(itemToAdd);
                print(" ES Count  " +  equippableSlots.Count +  " ES index" + equippableSlots[i]);

                EquippableSlot tempSlot = equippableSlots[i];
                equippableSlots[i].button.onClick.AddListener(delegate { RemoveItem(tempSlot.item); });
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (!itemToRemove) return;
        for (int i = 0; i < equippableSlots.Count; i++)
        {
            if (itemToRemove.equippableSlotType == equippableSlots[i].equippableSlotType)
            {
                inventory.AddItem(itemToRemove);
                characterCustomization.EquipDefaultItem(itemToRemove.equippableSlotType);
                EquippableSlot tempSlot = equippableSlots[i];
                equippableSlots[i].button.onClick.RemoveListener(delegate { RemoveItem(tempSlot.item); });
                equippableSlots[i].RemoveItem();
            }
        }
    }
}
