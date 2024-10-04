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

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");

    protected override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (curHp <= 0)
        {
            gameObject.SetActive(false);
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
        if (!isAttacking && !returningToOriginalPosition) // ���� ���̰ų� ���ư��� ���� �ƴϸ�
        {
            if (gauge >= 5)
            {
                SpecialMove(); // �ʻ�� ���
            }
            else
            {
                StartCoroutine(PerformAttack()); // �Ϲ� ����
                IncreaseGauge(); // ������ ����
            }

            animator.SetTrigger(hashAttack);
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
            TestEnemy enemy = targetEnemy.GetComponent<TestEnemy>();
            if (enemy != null)
            {
                DealDamage(enemy, false);
            }

            // �ִϸ��̼��� ���� ������ ���
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // ���� ��ġ�� ���ư���
            while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * attackMoveSpeed);
                yield return null;
            }

            returningToOriginalPosition = false; // ��ġ ���� �Ϸ�
        }

        isAttacking = false; // ���� ����
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

        // ������ UI ������Ʈ
        GaugeManager.Instance.ResetGauge(this.transform);
    }
}
