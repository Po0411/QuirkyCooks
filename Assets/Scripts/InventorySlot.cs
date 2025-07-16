using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Components")]
    public Image backgroundImage;
    public Image itemImage;
    public TextMeshProUGUI amountText;

    [Header("Background Sprites")]
    public Sprite normalBackground;
    public Sprite selectedBackground;

    private ItemData currentItem;
    private int itemCount = 0;

    public void SetItem(ItemData item)
    {
        currentItem = item;
        itemCount = 1;
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
        UpdateText();
    }

    public void AddCount()
    {
        itemCount++;
        UpdateText();
    }

    public void RemoveCount(int count = 1)
    {
        itemCount = Mathf.Max(0, itemCount - count);
        if (itemCount == 0)
            ClearSlot();
        else
            UpdateText();
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemCount = 0;
        itemImage.enabled = false;
        amountText.text = "";
    }

    public void SetSelected(bool isSelected)
    {
        backgroundImage.sprite = isSelected ? selectedBackground : normalBackground;
    }

    public bool IsEmpty() => currentItem == null;
    public bool Matches(ItemData item) => currentItem == item;
    public bool IsFull() => currentItem != null && itemCount >= currentItem.maxStack;
    public int GetCount() => itemCount;

    /// <summary>현재 슬롯에 있는 아이템을 반환합니다 (없으면 null)</summary>
    public ItemData GetCurrentItem() => currentItem;

    private void UpdateText()
    {
        amountText.text = "x" + itemCount;
    }
}