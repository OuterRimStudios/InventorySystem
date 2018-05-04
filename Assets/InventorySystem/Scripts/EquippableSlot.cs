using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippableSlot : MonoBehaviour
{
    public Image icon;
    public Sprite defaultIcon;
    public EquippableSlotType equippableSlotTypeSO;
    [HideInInspector] public Item item;
    [HideInInspector] public string equippableSlotType;
    [HideInInspector] public int equippableSlotTypeIndex;
    [HideInInspector] public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void UpdateItem(Item _item)
    {
        item = _item;
        UpdateUI();
    }

    public void UpdateUI()
    {
        icon.sprite = item.uiIcon;
    }

    public void RemoveItem()
    {
        icon.sprite = defaultIcon;
        item = null;
    }
}
