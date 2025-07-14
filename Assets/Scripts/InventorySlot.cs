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

    private ItemData item;
    private int count;

    public void SetSelected(bool selected)
    {
        backgroundImage.sprite = selected ? selectedBackground : normalBackground;
    }

    public void SetItem(ItemData newItem)
    {
        item = newItem;
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
        count = 1;
        amountText.text = "x" + count;
    }

    public void AddCount()
    {
        count++;
        amountText.text = "x" + count;
    }

    public bool IsEmpty()
    {
        return item == null;
    }

    public ItemData GetItem()
    {
        return item;
    }

    public bool Matches(ItemData targetItem)
    {
        return item == targetItem;
    }
}
