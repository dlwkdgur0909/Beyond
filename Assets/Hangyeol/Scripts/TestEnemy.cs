using UnityEngine;

public class TestEnemy : Character
{
    void Start()
    {
        curHp = maxHp;
    }

    void Update()
    {
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
