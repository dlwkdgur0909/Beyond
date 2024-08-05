using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Attack,
    Defense,
    Ultimate
}

public class CardManager : MonoBehaviour
{
    public static CardManager Instance
    {
        get => instance;
        set
        {
            if (value == null)
                instance = null;
            else if (instance == null)
                instance = value;
            else if (instance != value)
                Destroy(value);
        }
    }
    private static CardManager instance;

    [Header("공격카드")]
    [SerializeField] private List<testCard> attackCards = new List<testCard>();

    [Header("방어카드")]
    [SerializeField] private List<testCard> defenseCards = new List<testCard>();

    [Header("궁극기카드")]
    [SerializeField] private List<testCard> ultimateCards = new List<testCard>();

    [Header("선택한 카드")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();

    [Header("현재 가지고 있는 카드")]
    [SerializeField] private List<testCard> currentCard = new List<testCard>();

    #region GetCard
    public void GetCard(int cardCount)
    {
        int ranCardType;
        int ranChar;

        while(cardCount <= 0)
        {
            // 랜덤한 캐릭터
            ranChar = Random.Range(0, StageManager.Instance.Players.Count);

            // 랜덤 카드 타입
            ranCardType = Random.Range(0, 2);
            
            if(ranCardType == 0)
            {
                currentCard.Add(attackCards[ranChar]);
            }
            else if(ranCardType == 1)
            {
                currentCard.Add(defenseCards[ranChar]);
            }

            cardCount--;
        }

        // Shuffle the cards list
        //for (int i = 0; i < cards.Count; i++)
        //{
        //    string temp = cards[i];
        //    int randomIndex = Random.Range(0, cards.Count);
        //    cards[i] = cards[randomIndex];
        //    cards[randomIndex] = temp;
        //}

        //// Print the shuffled cards
        //for (int i = 0; i < cards.Count; i++)
        //{
        //    Debug.Log(cards[i]);
        //}
    }
    #endregion

    #region SelectCard
    public void SelectCard(int index)
    {

    }
    #endregion

    public void MoveCard()
    {
        selectCards.Add(null);
    }
}
