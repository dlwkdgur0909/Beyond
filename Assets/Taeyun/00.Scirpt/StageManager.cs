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

    [Header("��������")]
    [SerializeField] private int curStage = 0;
    public int stageCount => curStage;

    [Header("��")]
    [SerializeField] private CurTurn curTurn;

    [Header("ī��")]
    [SerializeField] private int getCardCount;
    public int cardGetCount => getCardCount;

    [Header("���� ĳ����")]
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
        // �÷��̾� ĳ���� ����
    }

    private void SetCard()
    {
        // ī�� ����
    }
    #endregion

    #region Spawn
    private void SpawnEnemy()
    {
        // ���� �����ϴ� �Լ�
    }
    
    private void SpawnPlayerCharater()
    {
        // �÷��̾� ĳ���� �����ϴ� �Լ�
    }

    private void SpawnBoss()
    {
        // ���� ���� �Լ�
    }
    #endregion

    private void GetCard()
    {
        // ī�带 ��� �Լ�
    }

    private void MergeCard()
    {
        // ī�带 �����ִ� �Լ�
    }

    private void NextTurn()
    {
        // ���� ������ �̵��ϴ� �Լ�
    }
}