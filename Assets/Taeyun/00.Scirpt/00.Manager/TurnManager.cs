using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurTurnState
{
    Init,
    CardSelection,
    ActionExecution,
    ResultProcessing
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    [Header("ео")]
    [SerializeField] private CurTurnState curTurnState;


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
        curTurnState = CurTurnState.Init;
    }
}
