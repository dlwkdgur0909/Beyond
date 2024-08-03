using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Charactor : MonoBehaviour
{
    [Header("Status")]
    public float maxHp;
    public float curHp;
    public float dmg;
    public float specialDmg;
    public string attribute;   // 캐릭터들의 속성


    [Header("SpecialMove")]
    public float specialMove;
    public bool isSpecialMove = false;

    [Header("UI")]
    public Slider hpBar;
    public TextMeshProUGUI level;
    public Image specialMoveImage;

    protected Dictionary<string, Dictionary<string, float>> attributeEffectivenessDict;

    private void Start()
    {
        curHp = maxHp;
        UpdateHpBar();
        InitializeAttributeEffectiveness();
    }

    private void InitializeAttributeEffectiveness()
    {
        attributeEffectivenessDict = new Dictionary<string, Dictionary<string, float>>()
        {
            { "Fire", new Dictionary<string, float>
                {
                    { "Fire", 1.0f },
                    { "Water", 0.5f },
                    { "Grass", 2.0f }
                }
            },
            { "Water", new Dictionary<string, float>
                {
                    { "Fire", 2.0f },
                    { "Water", 1.0f },
                    { "Grass", 0.5f }
                }
            },
            { "Grass", new Dictionary<string, float>
                {
                    { "Fire", 0.5f },
                    { "Water", 2.0f },
                    { "Grass", 1.0f }
                }
            }
        };
    }

    protected float GetAttributeEffectiveness(string attackerAttribute, string defenderAttribute)
    {
        if (attributeEffectivenessDict.ContainsKey(attackerAttribute) && attributeEffectivenessDict[attackerAttribute].ContainsKey(defenderAttribute))
        {
            return attributeEffectivenessDict[attackerAttribute][defenderAttribute];
        }
        return 1.0f; // 기본 배율
    }
    public void TakeDamage(float amount)
    {
        curHp -= amount;
        if (curHp <= 0)
        {
            curHp = 0;
        }
        UpdateHpBar();
    }
    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.value = curHp / maxHp;
        }
    }

    public abstract void Attack();

    public abstract void SpecialMove();
}
