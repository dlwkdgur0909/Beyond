using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [Header("공격카드")]
    [SerializeField] private List<testCard> attackCards = new List<testCard>();

    [Header("방어카드")]
    [SerializeField] private List<testCard> defenseCards = new List<testCard>();

    [Header("궁극기카드")]
    [SerializeField] private List<testCard> ultimateCards = new List<testCard>();

    [Header("선택한 카드")]
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
