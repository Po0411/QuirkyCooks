using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP Slots")]
    [Tooltip("�� ü�� ĭ ���� (�⺻ 5)")]
    public int maxHpSlots = 5;
    private int currentHpSlots;

    [Tooltip("ü���� ǥ���� Image ���Ե� (�ޡ�� �������)")]
    public Image[] hpSegments;

    [Tooltip("ü�� ĭ�� ���� á�� �� ��������Ʈ")]
    public Sprite filledSprite;
    [Tooltip("ü�� ĭ�� ����� �� ��������Ʈ")]
    public Sprite emptySprite;

    void Start()
    {
        // �ʱ�ȭ
        currentHpSlots = maxHpSlots;
        UpdateHpUI();
    }

    /// <summary>
    /// ���� ������ �������� �����ϴ�.
    /// �� �� ���� ������ slots ������ŭ ü�� ĭ�� ���Դϴ�.
    /// </summary>
    /// <param name="slots">���� ĭ ���� (���� 1)</param>
    public void TakeDamageSlots(int slots)
    {
        currentHpSlots = Mathf.Clamp(currentHpSlots - slots, 0, maxHpSlots);
        UpdateHpUI();

        if (currentHpSlots == 0)
            Die();
    }

    /// <summary>
    /// HP ���� UI�� ä��/��ĭ ��������Ʈ�� �����մϴ�.
    /// </summary>
    private void UpdateHpUI()
    {
        for (int i = 0; i < hpSegments.Length; i++)
        {
            if (hpSegments[i] == null) continue;
            hpSegments[i].sprite = (i < currentHpSlots) ? filledSprite : emptySprite;
        }
    }

    /// <summary>
    /// �÷��̾� ü���� 0�� �Ǿ��� �� ȣ��˴ϴ�.
    /// </summary>
    private void Die()
    {
        // TODO: ��� ����Ʈ, ������, ���� ���� ȭ�� �� ���ϴ� ���� �߰�
        Debug.Log("Player has died.");
        // ����: �̵�/���� ��Ȱ��ȭ
        var controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;
    }
}