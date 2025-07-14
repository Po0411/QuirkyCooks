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
        // ���õ� ���� ���� (���콺 ��)
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

        // �׽�Ʈ: ������ �߰� (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItem);
        }
    }

    public ItemData testItem; // �ӽ� �׽�Ʈ�� ������

    public void AddItem(ItemData newItem)
    {
        // ���� ������ ���� ���� ã��
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsEmpty() && slot.Matches(newItem) && newItem.isStackable)
            {
                slot.AddCount();
                return;
            }
        }

        // �� ���� ã��
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(newItem);
                return;
            }
        }

        Debug.Log("�κ��丮 ���� ��");
    }

    void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(i == selectedIndex);
        }
    }
}
