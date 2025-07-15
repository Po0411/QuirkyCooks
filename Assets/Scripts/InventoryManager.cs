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
        // ���콺 �ٷ� ���� ���� ����
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
    /// ���Ե� �� ���õ� �ε����� �ش��ϴ� ���Ը� ���� ǥ���մϴ�.
    /// </summary>
    void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetSelected(i == selectedIndex);
    }

    /// <summary>
    /// �κ��丮�� �������� �߰��մϴ�. ���� ���� ������ �켱, �� ���Կ� �߰�.
    /// </summary>
    public void AddItem(ItemData newItem)
    {
        // 1) ���� ������ ���� ���� ���� ã��
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(newItem) && !slot.IsFull())
            {
                slot.AddCount();
                return;
            }
        }
        // 2) �� ���Կ� �߰�
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(newItem);
                return;
            }
        }
        Debug.Log("�κ��丮 ���� ��");
    }

    /// <summary>
    /// Ư�� �������� ���� ���� �̻� �ִ��� Ȯ���մϴ�.
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
    /// �κ��丮���� Ư�� �������� ���� ������ŭ �����մϴ�.
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