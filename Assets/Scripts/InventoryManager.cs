using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Slots")]
    public InventorySlot[] slots;
    public int selectedIndex = 0;

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
        foreach (var slot in slots)
            if (!slot.IsEmpty() && slot.Matches(newItem) && !slot.IsFull())
            {
                slot.AddCount();
                return;
            }
        foreach (var slot in slots)
            if (slot.IsEmpty())
            {
                slot.SetItem(newItem);
                return;
            }
        Debug.Log("인벤토리 가득 참");
    }

    public bool HasItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
            if (!slot.IsEmpty() && slot.Matches(item) && slot.GetCount() >= amount)
                return true;
        return false;
    }

    public bool RemoveItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
            if (!slot.IsEmpty() && slot.Matches(item))
            {
                slot.RemoveCount(amount);
                return true;
            }
        return false;
    }

    /// <summary>현재 선택된 슬롯의 아이템을 반환합니다.</summary>
    public ItemData GetSelectedItem()
    {
        if (selectedIndex >= 0 && selectedIndex < slots.Length)
            return slots[selectedIndex].GetCurrentItem();
        return null;
    }
}