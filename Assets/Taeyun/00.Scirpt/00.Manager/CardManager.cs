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

    [Header("캐릭터 카드 리스트")]
    [SerializeField] private List<testCard> attackCard;
    [SerializeField] private List<testCard> defenseCard;

    [Header("선택한 카드")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();  // 선택한 카드 리스트

    [Header("현재 가지고 있는 카드")]
    [SerializeField] private List<testCard> currentCard = new List<testCard>();

    [Header("카드 포지션")]
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
        // 'D' 키를 눌렀을 때 카드 생성
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetCard();  // 카드 스폰 로직
        }

        // 'S' 키를 눌렀을 때 선택된 카드들의 행동 실행
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectCards.Count == 3)  // 선택된 카드가 3장일 때만 실행
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

        for (int i = 0; i < cardIndex; i++) // 최대 7개의 카드를 생성
        {
            ranChar = Random.Range(0, StageManager.Instance.Players.Count);
            ranCardType = Random.Range(0, 2);  // Attack 또는 Defense 카드 선택

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

        // 카드 리스트 셔플
        Shuffle(cardInfoList);

        // 카드 생성 및 위치 설정
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

    #region 카드 선택 및 행동 실행
    public void SelectCard(testCard card)
    {
        if (selectCards.Count < 3)  // 3개 카드만 선택 가능
        {
            selectCards.Add(card);
            Debug.Log("Card selected: " + card.CardName);
        }
    }

    // 선택한 카드들의 행동을 순차적으로 실행하는 코루틴
    private IEnumerator ExecuteCardActions()
    {
        foreach (testCard card in selectCards)
        {
            card.UseSkill();  // 카드 행동 실행
            yield return new WaitForSeconds(1f);  // 각 행동 사이에 딜레이 추가 (조정 가능)
        }

        selectCards.Clear();  // 행동이 끝나면 선택한 카드 초기화
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


