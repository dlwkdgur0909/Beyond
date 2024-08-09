
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Attack,
    Defense
}
[System.Serializable]
public class CardInfo
{
    public GameObject cardObject;
    public testCard cardScript;
}

[System.Serializable]
public class CharCardData
{
    public CardInfo AttackCard;
    public CardInfo DefenseCard;
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

    [Header("ĳ���� ī�� ����Ʈ")]
    [SerializeField] private List<CharCardData> charCardData;

    [Header("������ ī��")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();

    [Header("���� ������ �ִ� ī��")]
    [SerializeField] private List<CardInfo> currentCard = new List<CardInfo>();
    public List<CardInfo> CurrentCard => currentCard;

    [Header("ī�� ������")]
    [SerializeField] private List<Transform> cardPos = new List<Transform>();
    [SerializeField] private Transform canvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetCard();
        }
    }

    private void CardInit()
    {
        foreach(CharCardData card in charCardData)
        {
            //charCardDa
        }
    }

    #region GetCard
    public void GetCard()
    {
        int ranCardType;
        int ranChar;
        List<CardInfo> cardInfoList = new List<CardInfo>();

        // ���� ������ �ִ� ī�� ����
        foreach (CardInfo spawnedCard in currentCard)
        {
            Destroy(spawnedCard.cardObject);
        }
        currentCard.Clear();

        // ������ ĳ������ ī�� ����
        for (int i = 0; i < 7; i++) // �ִ� 7���� ī�带 ����
        {
            ranChar = Random.Range(0, StageManager.Instance.Players.Count);
            ranCardType = Random.Range(0, 2);  // Attack �Ǵ� Defense ī�� ����

            if (ranCardType == (int)CardType.Attack)
            {
                cardInfoList.Add(charCardData[ranChar].AttackCard);
            }
            else if (ranCardType == (int)CardType.Defense)
            {
                cardInfoList.Add(charCardData[ranChar].DefenseCard);
            }
        }

        // ī�� ����Ʈ ����
        Shuffle(cardInfoList);

        // ī�� ���� �� ��ġ ����
        for (int i = 0; i < cardInfoList.Count; i++)
        {
            GameObject cardObj = Instantiate(cardInfoList[i].cardObject, cardPos[i].position, Quaternion.identity);
            cardObj.transform.SetParent(canvas.transform);
            currentCard.Add(cardInfoList[i]);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
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

    public void RemoveCard(testCard card)
    {
        foreach(CardInfo curCard in currentCard)
        {
            if(card == curCard.cardScript)
            {
                currentCard.Remove(curCard);
            }
        }
    }
}
