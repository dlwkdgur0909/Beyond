using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ����
/// </summary>
public enum CurTurnState
{
    /// <summary>
    /// ó�� �� ����
    /// </summary>
    Init,
    /// <summary>
    /// ī�� ����
    /// </summary>
    CardSelection,
    /// <summary>
    /// ������ �ൿ ����
    /// </summary>
    ActionExecution,
    /// <summary>
    /// �� �� ������ Ȯ��
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

    [Header("��")]
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
