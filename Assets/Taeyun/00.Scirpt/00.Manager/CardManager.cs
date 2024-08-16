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

    [Header("캐릭터 카드 리스트")]
    //[SerializeField] private List<testCard> charCardData;
    [SerializeField] private List<testCard> attackCard;
    [SerializeField] private List<testCard> defenseCard;

    [Header("선택한 카드")]
    [SerializeField] private List<testCard> selectCards = new List<testCard>();

    [Header("현재 가지고 있는 카드")]
    [SerializeField] private List<testCard> currentCard = new List<testCard>();
    public List<testCard> CurrentCard => currentCard;

    [Header("카드 포지션")]
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

        // 랜덤한 캐릭터의 카드 생성
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
        Debug.Log(cardInfoList.Count);


        // 카드 생성 및 위치 설정
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
    /// 카드의 레벨을 올리는 함수
    /// </summary>
    private void CardLevelUp()
    {
        for (int i = 0; i < currentCard.Count - 1; i++)
        {
            // 현재 카드와 다음 카드의 정보가 동일한지 확인
            if (currentCard[i].ReturnCardInfo() == currentCard[i + 1].ReturnCardInfo())
            {
                // 카드의 최대 레벨이 아닐 때 레벨업
                if (currentCard[i].ReturnCardInfo().Item1 == true)
                {
                    currentCard[i].SkillCardLevelUp(); // 첫 번째 카드 레벨업
                    Destroy(currentCard[i + 1].gameObject); // 두 번째 카드 삭제
                    currentCard.RemoveAt(i + 1); // 리스트에서 제거
                    i = -1; // 루프를 처음부터 다시 시작
                }
            }
        }

        // 카드 위치 재정렬
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