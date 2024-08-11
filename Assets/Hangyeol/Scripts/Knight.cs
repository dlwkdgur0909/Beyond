using System.Collections;
using UnityEngine;

public class Knight : Character
{
    public float specialMoveMultiplier = 2.0f; // 필살기 배율 (2배)
    public float attackMoveSpeed = 5.0f; // 공격 시 이동 속도

    private Vector3 originalPosition;
    private Transform targetEnemy;
    private bool isAttacking = false;
    private bool returningToOriginalPosition = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    // HP가 가장 적은 적을 찾는 메서드로 수정
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
            SpecialMove(); // 게이지가 5일 때 필살기 사용
        }
        else
        {
            StartCoroutine(PerformAttack()); // 일반 공격
            IncreaseGauge(); // 게이지 증가
        }
    }

    private IEnumerator PerformAttack()
    {
        Transform lowestHpEnemy = FindLowestHpEnemy();
        if (lowestHpEnemy != null)
        {
            originalPosition = transform.position;
            targetEnemy = lowestHpEnemy;
            isAttacking = true;

            // 적에게 이동할 때까지 대기
            yield return new WaitUntil(() => !isAttacking && !returningToOriginalPosition);
        }
    }

    private void MoveTowardsEnemy()
    {
        if (targetEnemy != null)
        {
            Vector3 enemyPosition = targetEnemy.position;

            // 적에게 이동
            if (Vector3.Distance(transform.position, enemyPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyPosition, Time.deltaTime * attackMoveSpeed);
            }
            else
            {
                // 적에게 도착 시 데미지 입히기
                TestEnemy enemy = targetEnemy.GetComponent<TestEnemy>();
                if (enemy != null)
                {
                    DealDamage(enemy);
                }

                // 공격 완료 후 원래 자리로 돌아가기
                isAttacking = false;
                returningToOriginalPosition = true;
            }
        }
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
        StartCoroutine(SpecialMoveCoroutine()); // 필살기를 코루틴으로 실행
    }

    private IEnumerator SpecialMoveCoroutine()
    {
        float originalDmg = dmg; // 원래 공격력을 저장
        dmg *= specialMoveMultiplier; // 공격력 2배로 증가

        // 필살기 공격 실행
        yield return StartCoroutine(PerformAttack());

        // 공격력 원상 복구
        dmg = originalDmg;
        gauge = 0; // 게이지 초기화

        // 게이지 UI를 업데이트 (GaugeManager에 ResetGauge 메서드를 호출)
        if (gaugeManager != null)
        {
            gaugeManager.ResetGauge(this.transform);
        }
    }
}
