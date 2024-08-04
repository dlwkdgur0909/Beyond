using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 턴 상태
/// </summary>
public enum CurTurnState
{
    /// <summary>
    /// 처음 턴 설정
    /// </summary>
    Init,
    /// <summary>
    /// 카드 선택
    /// </summary>
    CardSelection,
    /// <summary>
    /// 선택한 행동 실행
    /// </summary>
    ActionExecution,
    /// <summary>
    /// 턴 다 끝나고 확인
    /// </summary>
    ResultProcessing
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance
    {
        get => instance;
        set
        {
            if (value == null)
                instance = null;
            else if(instance == null) 
                instance = value;
            else if(instance != value)
                Destroy(value);
        }
    }
    private static TurnManager instance;

    [Header("턴")]
    [SerializeField] private CurTurnState curTurnState;


    private void Start()
    {
        curTurnState = CurTurnState.Init;
    }

    private void NextTurn()
    {
        switch (curTurnState)
        {
            case CurTurnState.Init:
                break;
            case CurTurnState.CardSelection:
                break;
            case CurTurnState.ActionExecution:
                break;
            case CurTurnState.ResultProcessing:
                break;
        }
    }
}
