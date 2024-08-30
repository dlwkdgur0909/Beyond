using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Gold;
    public float HP;
    public float maxHP;
    public bool isDeath;

    public void AncientGold()
    {
        Gold += 1000;
    }

    public void BigGoldBall()
    {
        Gold += 500;
    }

    public void GoldBall()
    {
        Gold += 100;
    }

    public void NormalMedicine()
    {
        HP = (maxHP / 100) * 10;
    }

    public void GoodMedicine()
    {
        HP = (maxHP / 100) * 25;
    }

    public void HighMedicine()
    {
        HP = (maxHP / 100) * 50;
    }

    public void PullMedicine()
    {
        HP = maxHP;
    }

    public void PieceEnergy()
    {
        //�ϴ� �����ϸ� �����ִ� â���� �̵��� ���� 
        //������ ������ ������ ������ ���ֿ��� ���
        if(isDeath)
        {
            isDeath = false;
            HP = maxHP / 50;
        } 

    }

    public void LumpEnergy()
    {
        //�ϴ� �����ϸ� �����ִ� â���� �̵��� ���� 
        //������ ������ ������ ������ ���ֿ��� ���
        if (isDeath)
        {
            isDeath = false;
            HP = maxHP;
        }
    }
}
