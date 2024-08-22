using System.Collections;
using UnityEngine;

public class Knight : Character
{
    public float specialMoveMultiplier = 2.0f; // �ʻ�� ���� (2��)
    public float attackMoveSpeed = 5.0f; // ���� �� �̵� �ӵ�

    private Vector3 originalPosition;
    private Transform targetEnemy;
    private bool isAttacking = false;
    private bool returningToOriginalPosition = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && !returningToOriginalPosition)
        {
            Attack();
        }

        if (curHp <= 0)
        {
            Destroy(gameObject);
        }

        if (isAttacking)
        {
            MoveTowardsEnemy();
        }
        else if (returningToOriginalPosition)
        {
            ReturnToOriginalPosition();
        }
    }

    private Transform FindLowestHpEnemy()
    {
        float minHp = Mathf.Infinity;
        Transform lowestHpEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            TestEnemy enemyComponent = enemy.GetComponent<TestEnemy>();
            if (enemyComponent != null && enemyComponent.curHp < minHp)
            {
                minHp = enemyComponent.curHp;
                lowestHpEnemy = enemy.transform;
            }
        }
        return lowestHpEnemy;
    }

    public override void Attack()
    {
        if (gauge >= 5)
        {
            SpecialMove(); // �������� 5�� �� �ʻ�� ���
        }
        else
        {
            StartCoroutine(PerformAttack()); // �Ϲ� ����
            IncreaseGauge(); // ������ ����
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        Transform closestEnemy = FindLowestHpEnemy();
        if (closestEnemy != null)
        {
            originalPosition = transform.position;
            targetEnemy = closestEnemy;

            // ������ �̵�
            while (Vector3.Distance(transform.position, targetEnemy.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, Time.deltaTime * attackMoveSpeed);
                yield return null;
            }

            // ������ ���� �� ������ ������
            TestEnemy enemy = targetEnemy.GetComponent<TestEnemy>();
            if (enemy != null)
            {
                DealDamage(enemy);
            }

            // ���� �Ϸ� �� ���� �ڸ��� ���ư���
            returningToOriginalPosition = true;
            isAttacking = false;
        }
    }

    private void MoveTowardsEnemy()
    {
        // �� �κ��� PerformAttack���� ó����
    }

    private void ReturnToOriginalPosition()
    {
        if (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * attackMoveSpeed);
        }
        else
        {
            returningToOriginalPosition = false;
        }
    }
		
public override void SpecialMove()
{
	StartCoroutine(SpecialMoveCoroutine());
}
 
    private IEnumerator SpecialMoveCoroutine()
    {
        float originalDmg = dmg; // ���� ���ݷ��� ����
        dmg *= specialMoveMultiplier; // ���ݷ� 2��� ����

        // �ʻ�� ���� ����
        yield return StartCoroutine(PerformAttack());

        // ���ݷ� ���� ����
        dmg = originalDmg;
        gauge = 0; // ������ �ʱ�ȭ

        // ������ UI�� ������Ʈ (GaugeManager�� ResetGauge �޼��带 ȣ��)
        if (gaugeManager != null)
        {
            gaugeManager.ResetGauge(this.transform);
        }
    }
}
