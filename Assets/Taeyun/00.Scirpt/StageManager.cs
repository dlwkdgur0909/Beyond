using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurTurn
{
    Init,
    SelectCard,
    PlayTurn,
    CheckGameState
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("스테이지")]
    [SerializeField] private int curStage = 0;
    public int stageCount => curStage;

    [Header("턴")]
    [SerializeField] private CurTurn curTurn;

    [Header("카드")]
    [SerializeField] private int getCardCount;
    public int cardGetCount => getCardCount;

    [Header("현재 캐릭터")]
    [SerializeField] private List<testPlayer> testPlayers = new List<testPlayer>();
    [SerializeField] private List<testMonster> testMonster = new List<testMonster>();
    [SerializeField] private List<testCard> testCard = new List<testCard>();

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

    private void Start()
    {
        NextStage();
    }

    private void NextStage()
    {
        switch (curTurn)
        {
            case CurTurn.Init:
                SetPlayerCharacter();
                SetCard();
                curTurn = CurTurn.SelectCard;
                break;
            case CurTurn.SelectCard:
                GetCard();
                curTurn = CurTurn.PlayTurn;
                break;
            case CurTurn.PlayTurn:
                curTurn = CurTurn.CheckGameState;
                break;
            case CurTurn.CheckGameState:
                curTurn = CurTurn.SelectCard;
                break;
        }
    }

    #region Set
    private void SetPlayerCharacter()
    {
        // 플레이어 캐릭터 설정
    }

    private void SetCard()
    {
        // 카드 설정
    }
    #endregion

    #region Spawn
    private void SpawnEnemy()
    {
        // 적을 생성하는 함수
    }
    
    private void SpawnPlayerCharater()
    {
        // 플레이어 캐릭터 생성하는 함수
    }

    private void SpawnBoss()
    {
        // 보스 생성 함수
    }
    #endregion

    private void GetCard()
    {
        // 카드를 얻는 함수
    }

    private void MergeCard()
    {
        // 카드를 합쳐주는 함수
    }

    private void NextTurn()
    {
        // 다음 턴으로 이동하는 함수
    }
}