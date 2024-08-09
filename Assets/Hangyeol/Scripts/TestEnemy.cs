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
