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
    public static CardManager Instance { get; set; }

    [Header("ĳ���� ī�� ����Ʈ")]
    [SerializeField] private List<testCard> attackCard;
    [SerializeField] private List<testCard> defenseCard;

    [Header("������ ī��")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();  // ������ ī�� ����Ʈ

    [Header("���� ������ �ִ� ī��")]
    [SerializeField] private List<testCard> currentCard = new List<testCard>();

    [Header("ī�� ������")]
    [SerializeField] private List<Transform> cardPos = new List<Transform>();
    [SerializeField] private Transform canvas;

    private void Awake()
    {
        
        Instance = this;
    }

    private void OnDisable()
    {
        selectCards.Clear();
        currentCard.Clear();
    }

    private void Update()
    {
        // 'D' Ű�� ������ �� ī�� ����
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetCard();  // ī�� ���� ����
        }

        // 'S' Ű�� ������ �� ���õ� ī����� �ൿ ����
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectCards.Count == 3)  // ���õ� ī�尡 3���� ���� ����
            {
                StartCoroutine(ExecuteCardActions());
            }
        }
    }

    #region GetCard
    public void GetCard()
    {
        int ranCardType;
        int ranChar;
        List<testCard> cardInfoList = new List<testCard>();

        int cardIndex = 7 - currentCard.Count;

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

        // ī�� ���� �� ��ġ ����
        for (int i = 0; i < 7; i++)
        {
            if (cardInfoList.Count > i)
            {
                currentCard.Add(cardInfoList[i]);
                cardInfoList[i].gameObject.transform.SetParent(canvas.transform);
            }
            currentCard[i].transform.position = cardPos[i].position;
        }

        CardLevelUp();
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

    private void CardLevelUp()
    {
        for (int i = 0; i < currentCard.Count - 1; i++)
        {
            if (currentCard[i].ReturnCardInfo() == currentCard[i + 1].ReturnCardInfo())
            {
                if (currentCard[i].ReturnCardInfo().Item1 == true)
                {
                    currentCard[i].SkillCardLevelUp();
                    Destroy(currentCard[i + 1].gameObject);
                    currentCard.RemoveAt(i + 1);
                    i = -1;
                }
            }
        }

        for (int i = 0; i < currentCard.Count; i++)
        {
            currentCard[i].transform.position = cardPos[i].position;
        }
    }

    #region ī�� ���� �� �ൿ ����
    public void SelectCard(testCard card)
    {
        if (selectCards.Count < 3)  // 3�� ī�常 ���� ����
        {
            selectCards.Add(card);
            Debug.Log("Card selected: " + card.CardName);
        }
    }

    // ������ ī����� �ൿ�� ���������� �����ϴ� �ڷ�ƾ
    private IEnumerator ExecuteCardActions()
    {
        foreach (testCard card in selectCards)
        {
            card.UseSkill();  // ī�� �ൿ ����
            yield return new WaitForSeconds(1f);  // �� �ൿ ���̿� ������ �߰� (���� ����)
        }

        selectCards.Clear();  // �ൿ�� ������ ������ ī�� �ʱ�ȭ
    }
    #endregion

    public void RemoveCard(GameObject removeCard)
    {
        foreach (testCard card in currentCard)
        {
            if (card.gameObject == removeCard)
            {
                Debug.Log("Card found, removing...");
                currentCard.Remove(card);
                Destroy(card.gameObject);
                break;
            }
        }
    }
}


