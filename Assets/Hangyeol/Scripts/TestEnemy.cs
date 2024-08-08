using UnityEngine;

public class TestEnemy : Character
{


    void Update()
    {
        if(curHp <= 0)
        {
            Destroy(gameObject);
        }
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
                DealDamage(player);
            }
        }
    }
}
