using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Slots")]
    public InventorySlot[] slots;
    public int selectedIndex = 3;

    void Start()
    {
        UpdateSlotHighlight();
    }

    void Update()
    {
        // 마우스 휠로 선택 슬롯 변경
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

    /// <summary>
    /// 슬롯들 중 선택된 인덱스에 해당하는 슬롯만 강조 표시합니다.
    /// </summary>
    void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetSelected(i == selectedIndex);
    }

    /// <summary>
    /// 인벤토리에 아이템을 추가합니다. 스택 가능 슬롯을 우선, 빈 슬롯에 추가.
    /// </summary>
    public void AddItem(ItemData newItem)
    {
        // 1) 같은 아이템 스택 가능 슬롯 찾기
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(newItem) && !slot.IsFull())
            {
                slot.AddCount();
                return;
            }
        }
        // 2) 빈 슬롯에 추가
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(newItem);
                return;
            }
        }
        Debug.Log("인벤토리 가득 참");
    }

    /// <summary>
    /// 특정 아이템이 지정 개수 이상 있는지 확인합니다.
    /// </summary>
    public bool HasItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(item) && slot.GetCount() >= amount)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 인벤토리에서 특정 아이템을 지정 개수만큼 제거합니다.
    /// </summary>
    public bool RemoveItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(item))
            {
                slot.RemoveCount(amount);
                return true;
            }
        }
        return false;
    }
}