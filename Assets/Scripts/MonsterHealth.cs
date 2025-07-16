using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [Header("HP Settings")]
    public float maxHp = 5000f;
    private float currentHp;

    [Header("Death Drop")]
    [Tooltip("������ ������ ��� ������ (Rigidbody �ʿ�)")]
    public GameObject meatPrefab;
    [Tooltip("��Ⱑ Ƣ�� ���� �ʱ� ���� ����")]
    public float spawnForce = 5f;
    [Tooltip("��⸦ ������ ���� ������")]
    public Vector3 spawnOffset = new Vector3(0, 1f, 0);

    void Start()
    {
        currentHp = maxHp;
    }

    /// <summary>
    /// �ܺο��� ���ظ� ���� �� ȣ��
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
            Debug.LogWarning("MonsterHealth: meatPrefab�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ��ġ + �����¿� ����
        Vector3 spawnPos = transform.position + spawnOffset;
        GameObject meat = Instantiate(meatPrefab, spawnPos, Random.rotation);

        // Rigidbody�� ������ ���� + ���� ���� �� �ֱ�
        Rigidbody rb = meat.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // ���� Ƣ������� ���� + �ణ ������ ����
            Vector3 force = Vector3.up * spawnForce
                          + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * spawnForce * 0.5f;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}