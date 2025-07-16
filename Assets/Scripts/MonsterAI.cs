using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { DaeduMabel, Tomatok, Krang }

[RequireComponent(typeof(CharacterController))]
public class MonsterAI : MonoBehaviour
{
    [Header("Type & References")]
    public MonsterType type;
    public Transform player;
    public string safeZoneTag = "SafeZone";

    [Header("Detection Ranges (m)")]
    public float suspiciousRange = 10f;
    public float approachRange = 8f;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public float closeAttackRange = 1f;

    [Header("Movement & Attack")]
    public float moveSpeed = 1.2f;   // DaeduMabel/Tomatok �⺻��
    public float krangMoveSpeed = 1.5f;   // Krang ����
    public float attackInterval = 0.2f;   // DaeduMabel/Tomatok �⺻��
    public float krangAttackInterval = 0.3f;   // Krang ����
    public float stunDuration = 2f;     // Krang ���� �ð�

    private CharacterController cc;
    private Vector3 spawnPos;
    private float lastAttackTime;
    private bool inSafeZone;
    private bool isStunned;

    enum State { Idle, Suspicious, Approach, Chase, Attack, Stunned, Returning }
    private State state = State.Idle;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        spawnPos = transform.position;

        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (isStunned) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // �߰� ���� ����: ���� ���� ���� OR �־���(suspiciousRange �ʰ�)
        if (inSafeZone || dist > suspiciousRange)
        {
            state = State.Returning;
            MoveTowards(spawnPos);
            if (Vector3.Distance(transform.position, spawnPos) < 0.1f)
                state = State.Idle;
            return;
        }

        // ���� ��ȯ (�Ÿ� ����)
        if (dist > approachRange) state = State.Suspicious;
        else if (dist > chaseRange) state = State.Approach;
        else if (dist > attackRange) state = State.Chase;
        else state = State.Attack;

        // �ൿ
        switch (state)
        {
            case State.Suspicious:
                // TODO: �ǽ� �ִϸ��̼�
                break;
            case State.Approach:
            case State.Chase:
                MoveTowards(player.position);
                break;
            case State.Attack:
                TryAttack();
                break;
        }
    }

    void MoveTowards(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        float speed = (type == MonsterType.Krang) ? krangMoveSpeed : moveSpeed;
        cc.SimpleMove(dir * speed);
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    void TryAttack()
    {
        float interval = (type == MonsterType.Krang) ? krangAttackInterval : attackInterval;
        if (Time.time - lastAttackTime < interval) return;
        lastAttackTime = Time.time;

        // PlayerHealth ������Ʈ ��������
        PlayerHealth ph = player.GetComponent<PlayerHealth>()
                         ?? player.GetComponentInChildren<PlayerHealth>();
        if (ph == null) return;

        if (type == MonsterType.Krang)
        {
            // ũ��: ���ϸ�
            StartCoroutine(StunPlayer());
        }
        else
        {
            // ��θ���/�丶��: ���� ��ġ�� �� 1ĭ ����
            ph.TakeDamageSlots(1);
        }
    }

    IEnumerator StunPlayer()
    {
        isStunned = true;
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null) pc.enabled = false;

        yield return new WaitForSeconds(stunDuration);

        if (pc != null) pc.enabled = true;
        isStunned = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(safeZoneTag))
            inSafeZone = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(safeZoneTag))
            inSafeZone = false;
    }
}
