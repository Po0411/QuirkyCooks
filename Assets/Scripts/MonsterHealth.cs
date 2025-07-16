using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [Header("HP Settings")]
    public float maxHp = 5000f;
    private float currentHp;

    [Header("Death Drop")]
    [Tooltip("죽으면 생성할 고기 프리팹 (Rigidbody 필요)")]
    public GameObject meatPrefab;
    [Tooltip("고기가 튀어 오를 초기 힘의 세기")]
    public float spawnForce = 5f;
    [Tooltip("고기를 생성할 높이 오프셋")]
    public Vector3 spawnOffset = new Vector3(0, 1f, 0);

    void Start()
    {
        currentHp = maxHp;
    }

    /// <summary>
    /// 외부에서 피해를 입힐 때 호출
    /// </summary>
    public void TakeDamage(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0f)
            Die();
    }

    void Die()
    {
        SpawnMeat();
        Destroy(gameObject);
    }

    void SpawnMeat()
    {
        if (meatPrefab == null)
        {
            Debug.LogWarning("MonsterHealth: meatPrefab이 할당되지 않았습니다.");
            return;
        }

        // 몬스터 위치 + 오프셋에 생성
        Vector3 spawnPos = transform.position + spawnOffset;
        GameObject meat = Instantiate(meatPrefab, spawnPos, Random.rotation);

        // Rigidbody가 있으면 위로 + 랜덤 방향 힘 주기
        Rigidbody rb = meat.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 위로 튀어오르는 벡터 + 약간 랜덤한 방향
            Vector3 force = Vector3.up * spawnForce
                          + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * spawnForce * 0.5f;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}