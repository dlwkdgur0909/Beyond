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
        // �ʿ��� �Ӽ��� �߰�
    };

    public static float GetEffectiveness(string attackerType, string defenderType)
    {
        if (effectiveness.ContainsKey(attackerType) && effectiveness[attackerType].ContainsKey(defenderType))
        {
            return effectiveness[attackerType][defenderType];
        }
        return 1.0f; // �⺻ ȿ���� 1.0
    }
}

public abstract class Character : MonoBehaviour
{
    [Header("Status")]
    public float maxHp;
    public float curHp;
    public float dmg;
    public string attribute; // ĳ������ �Ӽ�

    [Header("SpecialMove")]
    public float specialMove;
    public bool isSpecialMove = false;

    [Header("UI")]
    public TextMeshProUGUI level;
    public Slider hpSlider;
    public GaugeManager gaugeManager;

    [Header("Gauge")]
    public int gauge = 0; // ���� ������ ��



    protected virtual void OnEnable()
    {
        curHp = maxHp;
        UpdateHpUI();
    }

    private void Update()
    {
        // HP �ٸ� �ǽð����� ������Ʈ
        UpdateHpUI();
    }

    public void TakeDamage(float amount, bool isEnemy)
    {
        curHp -= amount;
        curHp = Mathf.Clamp(curHp, 0, maxHp); // HP�� 0 ���Ϸ� �������� �ʵ��� ����
        UpdateHpUI();

        if (curHp <= 0)
        {
            if (!isEnemy)
            {
                OnDeath();
            }
            else if(isEnemy)
            {
                StageManager.Instance.EnemyDie(gameObject.GetComponent<TestEnemy>());
            }
        }
    }

    public void DealDamage(Character target, bool isEnemy)
    {
        float effectiveness = TypeEffectiveness.GetEffectiveness(this.attribute, target.attribute);
        float damage = dmg * effectiveness;
        target.TakeDamage(damage, isEnemy);
    }

    public abstract void Attack();


    public abstract void SpecialMove();


    protected virtual void OnDeath()
    {
        // ��� �� ó�� ���� (��: ĳ���� ��Ȱ��ȭ, ��� �ִϸ��̼� ���� ��)
        Debug.Log($"{gameObject.name} has died.");
    }

    private void UpdateHpUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = curHp / maxHp;
        }
    }

    public void IncreaseGauge()
    {
        if (gaugeManager != null)
        {
            gaugeManager.IncreaseGauge(transform);
        }
    }
}
