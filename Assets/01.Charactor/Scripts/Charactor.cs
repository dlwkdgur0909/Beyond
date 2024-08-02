using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Charactor : MonoBehaviour
{
    [Header("Status")]  // 스테이터스
    public float maxHp;
    public float curHp;
    public float dmg;
    public float specialDmg;

    [Header("SpecialMove")] // 필살기
    public float specialMove;
    public bool isSpecialMove = false;

    [Header("UI")]
    public Slider hpBar;
    public TextMeshProUGUI level;
    public Image specialMoveImage;

    /// <summary>
    /// 데미지 주는 함수
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
