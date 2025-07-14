using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public int selectedIndex = 3;

    void Start()
    {
        UpdateSlotHighlight();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            selectedIndex = (selectedIndex + 1) % slots.Length;
            UpdateSlotHighlight();
        }
        else if (scroll < 0f)
        {
            selectedIndex = (selectedIndex - 1 + slots.Length) % slots.Length;
            UpdateSlotHighlight();
        }
    }

    void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetSelected(i == selectedIndex);
    }

    public void AddItem(ItemData newItem)
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(newItem) && !slot.IsFull())
            {
                slot.AddCount();
                return;
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(newItem);
                return;
            }
        }

        Debug.Log("인벤토리 가득 참");
    }
}