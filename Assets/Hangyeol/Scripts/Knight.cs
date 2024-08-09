using System.Collections;
using UnityEngine;

public class Knight : Character
{


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
    }

    private Transform FindClosestEnemy()
    {
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }

    public override void Attack()
    {
        StartCoroutine(PerformAttack());
        IncreaseGauge();
    }

    private IEnumerator PerformAttack()
    {
        Transform closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 originalPosition = transform.position;
            Vector3 enemyPosition = closestEnemy.position;

            while (Vector3.Distance(transform.position, enemyPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyPosition, Time.deltaTime * attackMove);
                yield return null;
            }

            TestEnemy enemy = closestEnemy.GetComponent<TestEnemy>();
            if (enemy != null)
            {
                DealDamage(enemy);
            }

            while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * attackMove);
                yield return null;
            }
        }
    }

    public override void SpecialMove()
    {
        // 필살기 구현
    }
}
