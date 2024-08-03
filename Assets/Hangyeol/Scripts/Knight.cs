using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Charactor
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float attackRange = 2f; // ���� ����
    private Vector3 originalPosition; // ���� ��ġ
    public LayerMask enemyLayer; // �� ���̾�
    public float detectionRadius = 10f; // �� Ž�� �ݰ�

    void Start()
    {
        originalPosition = transform.position; // ���� �� ���� ��ġ ����
    }

    void Update()
    {
        // ������Ʈ �������� �ֱ������� ���� ����� �� Ž�� �� ����
        if (Input.GetKeyDown(KeyCode.Space)) // ���� Ʈ���� (��: �����̽��ٸ� ������ ��)
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
        // Ư�� �̵� ����
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

        // ������ �ٰ�����
        while (Vector3.Distance(transform.position, enemyPosition) > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("����!");

        Charactor enemy = enemyTarget.GetComponent<Charactor>();
        if (enemy != null)
        {
            float damage = dmg * GetAttributeEffectiveness(attribute, enemy.attribute);
            enemy.TakeDamage(damage);
        }

        // ���� �� �ణ�� ���� (��: ���� �ִϸ��̼��� �Ϸ�� ������ ��ٸ�)
        yield return new WaitForSeconds(0.5f);

        // ���� ��ġ�� ���ư���
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // ��ġ ����
        transform.position = originalPosition;
    }
}
