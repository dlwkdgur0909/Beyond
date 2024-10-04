using UnityEngine;
using System.Collections;

public class TestEnemy : Character
{
    public float specialMoveMultiplier = 2.0f; // �ʻ�� ���� (2��)
    public float attackMoveSpeed = 5.0f; // ���� �� �̵� �ӵ�

    private Vector3 originalPosition;
    private Transform targetEnemy;
    private bool isAttacking = false;
    private bool returningToOriginalPosition = false;

    void Update()
    {
        if (curHp <= 0)
        {
            gameObject.SetActive(false);
        }

        // �����̽��ٸ� ������ ���� ����, ���� ���̰ų� ���� ��ġ�� ���ƿ��� ���̸� ���� �Ұ�
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && !returningToOriginalPosition)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private Transform FindLowestHpEnemy()
    {
        float minHp = Mathf.Infinity;
        Transform lowestHpEnemy = null;
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject Charactor in Player)
        {
            Knight enemyComponent = Charactor.GetComponent<Knight>();
            if (enemyComponent != null && enemyComponent.curHp < minHp)
            {
                minHp = enemyComponent.curHp;
                lowestHpEnemy = Charactor.transform;
            }
        }
        return lowestHpEnemy;
    }

    public override void Attack()
    {
        // ���� ���� ���� ����
    }

    public override void SpecialMove()
    {
        // ���� �ʻ�� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Knight player = other.GetComponent<Knight>();
            if (player != null)
            {
                DealDamage(player, false);
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true; // ���� ����
        Transform closestEnemy = FindLowestHpEnemy();

        yield return new WaitForSeconds(0.1f);

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
            Knight knight = targetEnemy.GetComponent<Knight>();
            if (knight != null)
            {
                DealDamage(knight, false);
            }

            // ���� ��ġ�� ���ư���
            returningToOriginalPosition = true; // ���� ��ġ�� ���ƿ��� ��
            while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * attackMoveSpeed);
                yield return null;
            }
            returningToOriginalPosition = false; // ��ġ ���� �Ϸ�
        }

        isAttacking = false; // ���� ����, ���� ��ġ�� ���ƿ� �Ŀ��� ���� ����
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

        // ������ UI ������Ʈ
        GaugeManager.Instance.ResetGauge(this.transform);
    }
}
