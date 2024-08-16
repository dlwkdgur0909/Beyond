using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum CardType
{
    Attack,
    Defense
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
    //[SerializeField] private List<testCard> charCardData;
    [SerializeField] private List<testCard> attackCard;
    [SerializeField] private List<testCard> defenseCard;

    [Header("������ ī��")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();

    [Header("���� ������ �ִ� ī��")]
    [SerializeField] private List<testCard> currentCard = new List<testCard>();
    public List<testCard> CurrentCard => currentCard;

    [Header("ī�� ������")]
    [SerializeField] private List<Transform> cardPos = new List<Transform>();
    [SerializeField] private Transform canvas;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetCard();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            CardLevelUp();
        }
    }

    private void CardInit()
    {
        //foreach(CharCardData card in charCardData)
        //{
        //    //charCardDa
        //}
    }

    #region GetCard
    public void GetCard()
    {
        int ranCardType;
        int ranChar;
        List<testCard> cardInfoList = new List<testCard>();

        int cardIndex = 7 - currentCard.Count;

        // ������ ĳ������ ī�� ����
        for (int i = 0; i < cardIndex; i++) // �ִ� 7���� ī�带 ����
        {
            ranChar = Random.Range(0, StageManager.Instance.Players.Count);
            ranCardType = Random.Range(0, 2);  // Attack �Ǵ� Defense ī�� ����

            if (ranCardType == (int)CardType.Attack)
            {
                GameObject card = Instantiate(attackCard[ranChar].gameObject, cardPos[i].position, Quaternion.identity);
                cardInfoList.Add(card.GetComponent<testCard>());
            }
            else if (ranCardType == (int)CardType.Defense)
            {
                GameObject card = Instantiate(defenseCard[ranChar].gameObject, cardPos[i].position, Quaternion.identity);
                cardInfoList.Add(card.GetComponent<testCard>());
            }
        }

        // ī�� ����Ʈ ����
        Shuffle(cardInfoList);
        Debug.Log(cardInfoList.Count);


        // ī�� ���� �� ��ġ ����
        for (int i = 0; i < 7; i++)
        {
            //GameObject cardObj = Instantiate(cardInfoList[i].gameObject, cardPos[i].position, Quaternion.identity);
            if (cardInfoList.Count > i)
            {
                currentCard.Add(cardInfoList[i]);
                cardInfoList[i].gameObject.transform.SetParent(canvas.transform);
            }
            currentCard[i].transform.position = cardPos[i].position;
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

    /// <summary>
    /// ī���� ������ �ø��� �Լ�
    /// </summary>
    private void CardLevelUp()
    {
        for (int i = 0; i < currentCard.Count - 1; i++)
        {
            // ���� ī��� ���� ī���� ������ �������� Ȯ��
            if (currentCard[i].ReturnCardInfo() == currentCard[i + 1].ReturnCardInfo())
            {
                // ī���� �ִ� ������ �ƴ� �� ������
                if (currentCard[i].ReturnCardInfo().Item1 == true)
                {
                    currentCard[i].SkillCardLevelUp(); // ù ��° ī�� ������
                    Destroy(currentCard[i + 1].gameObject); // �� ��° ī�� ����
                    currentCard.RemoveAt(i + 1); // ����Ʈ���� ����
                    i = -1; // ������ ó������ �ٽ� ����
                }
            }
        }

        // ī�� ��ġ ������
        for (int i = 0; i < currentCard.Count; i++)
        {
            currentCard[i].transform.position = cardPos[i].position;
        }
    }


    #region SelectCard
    public void SelectCard(int index)
    {

    }
    #endregion

    public void MoveCard()
    {
        selectCards.Add(null);
    }

    public void RemoveCard(GameObject removeCard)
    {
        foreach (testCard card in currentCard)
        {
            if (card.gameObject == removeCard)
            {
                Debug.Log("Card found, removing...");
                currentCard.Remove(card);
                Destroy(card.gameObject);  // Ensure you're destroying the GameObject
                break;
            }
        }
    }
}