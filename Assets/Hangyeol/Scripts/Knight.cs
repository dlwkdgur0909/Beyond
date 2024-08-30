using System.Collections;
using UnityEngine;

public class Knight : Character
{
    public float specialMoveMultiplier = 2.0f; // 필살기 배율 (2배)
    public float attackMoveSpeed = 5.0f; // 공격 시 이동 속도

    private Vector3 originalPosition;
    private Transform targetEnemy;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");

    protected override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
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
            SpecialMove(); // 게이지가 5일 때 필살기 사용
        }
        else
        {
            StartCoroutine(PerformAttack()); // 일반 공격
            IncreaseGauge(); // 게이지 증가
        }
        animator.SetTrigger(hashAttack);
    }

    private IEnumerator PerformAttack()
    {
        Transform closestEnemy = FindLowestHpEnemy();
        if (closestEnemy != null)
        {
            originalPosition = transform.position;
            targetEnemy = closestEnemy;

            // 적에게 이동
            while (Vector3.Distance(transform.position, targetEnemy.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, Time.deltaTime * attackMoveSpeed);
                yield return null;
            }

            // 적에게 도착 시 데미지 입히기
            TestEnemy enemy = targetEnemy.GetComponent<TestEnemy>();
            if (enemy != null)
            {
                DealDamage(enemy, false);
            }

            yield return null;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

            while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * attackMoveSpeed);
                yield return null;
            }
        }
    }

    public override void SpecialMove()
    {
        StartCoroutine(SpecialMoveCoroutine());
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
        //if (gaugeManager != null)
        //{
            GaugeManager.Instance.ResetGauge(this.transform);
        //}
    }
}
