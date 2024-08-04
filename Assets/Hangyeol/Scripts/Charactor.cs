using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public static class TypeEffectiveness
{
    private static readonly Dictionary<string, Dictionary<string, float>> effectiveness = new Dictionary<string, Dictionary<string, float>>()
    {
        { "Fire", new Dictionary<string, float> { { "Fire", 0.5f }, { "Water", 0.5f }, { "Grass", 2.0f } } },
        { "Water", new Dictionary<string, float> { { "Fire", 2.0f }, { "Water", 0.5f }, { "Grass", 0.5f } } },
        { "Grass", new Dictionary<string, float> { { "Fire", 0.5f }, { "Water", 2.0f }, { "Grass", 0.5f } } }
        // 필요한 속성들 추가
    };

    public static float GetEffectiveness(string attackerType, string defenderType)
    {
        if (effectiveness.ContainsKey(attackerType) && effectiveness[attackerType].ContainsKey(defenderType))
        {
            return effectiveness[attackerType][defenderType];
        }
        return 1.0f; // 기본 효과는 1.0
    }
}

public abstract class Character : MonoBehaviour
{
    [Header("Status")]
    public float maxHp;
    public float curHp;
    public float dmg;
    public string attribute; // 캐릭터의 속성

    [Header("SpecialMove")]
    public float specialMove;
    public bool isSpecialMove = false;

    [Header("UI")]
    public TextMeshProUGUI level;

    private void Start()
    {
        curHp = maxHp;
    }

    private void Update()
    {
    }

    public void TakeDamage(float amount)
    {
        curHp -= amount;
    }

    public void DealDamage(Character target)
    {
        float effectiveness = TypeEffectiveness.GetEffectiveness(this.attribute, target.attribute);
        float damage = dmg * effectiveness;
        target.TakeDamage(damage);
    }

    public abstract void Attack();
    public abstract void SpecialMove();

    private void HpBar()
    {
    }
}
