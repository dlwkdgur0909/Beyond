using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Charactor : MonoBehaviour
{
    [Header("Status")]  // �������ͽ�
    public float maxHp;
    public float curHp;
    public float dmg;
    public float specialDmg;

    [Header("SpecialMove")] // �ʻ��
    public float specialMove;
    public bool isSpecialMove = false;

    [Header("UI")]
    public Slider hpBar;
    public TextMeshProUGUI level;
    public Image specialMoveImage;

    /// <summary>
    /// ������ �ִ� �Լ�
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        curHp -= amount;
    }

    private void Start()
    {
        curHp = maxHp;
    }

    public abstract void Attack();

    public abstract void SpecialMove();

}
