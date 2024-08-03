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
    public void GetCard(int numberOfCards)
    {
        int characterCount = 3;
        List<string> cards = new List<string>();

        for (int i = 1; i <= characterCount; i++)
        {
            for (int j = 0; j < numberOfCards; j++)
            {
                cards.Add($"Character {i} Attack");
                cards.Add($"Character {i} Defense");
            }
        }

        // Shuffle the cards list
        for (int i = 0; i < cards.Count; i++)
        {
            string temp = cards[i];
            int randomIndex = Random.Range(0, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        // Print the shuffled cards
        for (int i = 0; i < cards.Count; i++)
        {
            Debug.Log(cards[i]);
        }
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
