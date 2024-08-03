using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Charactor
{
    public float moveSpeed = 5f; // 이동 속도
    public float attackRange = 2f; // 공격 범위
    private Vector3 originalPosition; // 원래 위치
    public LayerMask enemyLayer; // 적 레이어
    public float detectionRadius = 10f; // 적 탐지 반경

    void Start()
    {
        originalPosition = transform.position; // 시작 시 원래 위치 저장
    }

    void Update()
    {
        // 업데이트 루프에서 주기적으로 가장 가까운 적 탐지 및 공격
        if (Input.GetKeyDown(KeyCode.Space)) // 공격 트리거 (예: 스페이스바를 눌렀을 때)
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Charactor enemy = other.GetComponent<Charactor>();
            if (enemy != null)
            {
                float damage = dmg * GetAttributeEffectiveness(attribute, enemy.attribute);
                enemy.TakeDamage(damage);
            }
        }
    }

    public override void SpecialMove()
    {
        // 특수 이동 구현
    }

    public override void Attack()
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            StartCoroutine(PerformAttack(nearestEnemy));
        }
    }

    private Transform FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = collider.transform;
            }
        }

        return nearestEnemy;
    }

    private IEnumerator PerformAttack(Transform enemyTarget)
    {
        Vector3 enemyPosition = enemyTarget.position;

        // 적에게 다가가기
        while (Vector3.Distance(transform.position, enemyPosition) > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("공격!");

        Charactor enemy = enemyTarget.GetComponent<Charactor>();
        if (enemy != null)
        {
            float damage = dmg * GetAttributeEffectiveness(attribute, enemy.attribute);
            enemy.TakeDamage(damage);
        }

        // 공격 후 약간의 지연 (예: 공격 애니메이션이 완료될 때까지 기다림)
        yield return new WaitForSeconds(0.5f);

        // 원래 위치로 돌아가기
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 위치 보정
        transform.position = originalPosition;
    }
}
