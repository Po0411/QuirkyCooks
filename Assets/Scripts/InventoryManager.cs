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
        // 선택된 슬롯 변경 (마우스 휠)
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

        // 테스트: 아이템 추가 (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItem);
        }
    }

    public ItemData testItem; // 임시 테스트용 아이템

    public void AddItem(ItemData newItem)
    {
        // 스택 가능한 기존 슬롯 찾기
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(newItem) && newItem.isStackable)
            {
                slot.AddCount();
                return;
            }
        }

        // 빈 슬롯 찾기
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

    void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(i == selectedIndex);
        }
    }
}
