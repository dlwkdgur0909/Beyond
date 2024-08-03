using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [Header("����ī��")]
    [SerializeField] private List<testCard> attackCards = new List<testCard>();

    [Header("���ī��")]
    [SerializeField] private List<testCard> defenseCards = new List<testCard>();

    [Header("�ñر�ī��")]
    [SerializeField] private List<testCard> ultimateCards = new List<testCard>();

    [Header("������ ī��")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectAttackCard(testPlayer player)
    {
        //player.Attack();
    }

    public void SelectDefensCard()
    {

    }

    public void SelectUltimateCard()
    {

    }

    public void MoveCard()
    {
        selectCards.Add(null);
    }
}
