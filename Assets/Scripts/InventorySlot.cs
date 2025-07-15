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

    /// <summary>
    /// 슬롯에 새 아이템을 설정합니다.
    /// </summary>
    public void SetItem(ItemData item)
    {
        currentItem = item;
        itemCount = 1;
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
        UpdateText();
    }

    /// <summary>
    /// 현재 슬롯의 아이템 수량을 1만큼 증가시킵니다.
    /// </summary>
    public void AddCount()
    {
        itemCount++;
        UpdateText();
    }

    /// <summary>
    /// 슬롯의 아이템을 지정한 수량만큼 제거합니다. 0이 되면 슬롯을 비웁니다.
    /// </summary>
    public void RemoveCount(int count = 1)
    {
        itemCount = Mathf.Max(0, itemCount - count);
        if (itemCount == 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateText();
        }
    }

    /// <summary>
    /// 슬롯을 완전히 초기화(비움)합니다.
    /// </summary>
    public void ClearSlot()
    {
        currentItem = null;
        itemCount = 0;
        itemImage.enabled = false;
        amountText.text = "";
    }

    /// <summary>
    /// 이 슬롯이 선택된 상태인지에 따라 배경을 변경합니다.
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        backgroundImage.sprite = isSelected ? selectedBackground : normalBackground;
    }

    public bool IsEmpty() => currentItem == null;
    public bool Matches(ItemData item) => currentItem == item;
    public bool IsFull() => currentItem != null && itemCount >= currentItem.maxStack;
    public int GetCount() => itemCount;

    private void UpdateText()
    {
        amountText.text = "x" + itemCount;
    }
}