using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image backgroundImage;
    public Image itemImage;
    public TextMeshProUGUI amountText;
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

    public void SetSelected(bool isSelected)
    {
        backgroundImage.sprite = isSelected ? selectedBackground : normalBackground;
    }

    public bool IsEmpty() => currentItem == null;

    public bool Matches(ItemData item) => currentItem != null && currentItem == item;

    public bool IsFull() => currentItem != null && itemCount >= currentItem.maxStack;

    private void UpdateText()
    {
        amountText.text = "x" + itemCount;
    }
}