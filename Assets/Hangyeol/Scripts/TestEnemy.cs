using UnityEngine;

public class TestEnemy : Character
{


    void Update()
    {
        if(curHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Attack()
    {
        // 적의 공격 로직 구현
    }

    public override void SpecialMove()
    {
        // 적의 필살기 구현
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Knight player = other.GetComponent<Knight>();
            if (player != null)
            {
                DealDamage(player);
            }
        }
    }
}
