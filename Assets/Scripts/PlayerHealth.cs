using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP Slots")]
    [Tooltip("총 체력 칸 개수 (기본 5)")]
    public int maxHpSlots = 5;
    private int currentHpSlots;

    [Tooltip("체력을 표시할 Image 슬롯들 (왼→오 순서대로)")]
    public Image[] hpSegments;

    [Tooltip("체력 칸이 가득 찼을 때 스프라이트")]
    public Sprite filledSprite;
    [Tooltip("체력 칸이 비었을 때 스프라이트")]
    public Sprite emptySprite;

    void Start()
    {
        // 초기화
        currentHpSlots = maxHpSlots;
        UpdateHpUI();
    }

    /// <summary>
    /// 슬롯 단위로 데미지를 입힙니다.
    /// 한 대 맞을 때마다 slots 개수만큼 체력 칸이 깎입니다.
    /// </summary>
    /// <param name="slots">깎을 칸 개수 (보통 1)</param>
    public void TakeDamageSlots(int slots)
    {
        currentHpSlots = Mathf.Clamp(currentHpSlots - slots, 0, maxHpSlots);
        UpdateHpUI();

        if (currentHpSlots == 0)
            Die();
    }

    /// <summary>
    /// HP 슬롯 UI를 채움/빈칸 스프라이트로 갱신합니다.
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
    /// 플레이어 체력이 0이 되었을 때 호출됩니다.
    /// </summary>
    private void Die()
    {
        // TODO: 사망 이펙트, 리스폰, 게임 오버 화면 등 원하는 로직 추가
        Debug.Log("Player has died.");
        // 예시: 이동/조작 비활성화
        var controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;
    }
}