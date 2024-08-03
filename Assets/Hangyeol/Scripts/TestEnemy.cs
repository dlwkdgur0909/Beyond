using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Charactor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialMove()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TestEnemy player = other.GetComponent<TestEnemy>();
            if (player != null)
            {
                float damage = dmg * GetAttributeEffectiveness(attribute, player.attribute);
                player.TakeDamage(damage);
            }
        }
    }
}
