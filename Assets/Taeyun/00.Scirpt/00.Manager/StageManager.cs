using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("스테이지")]
    [SerializeField] private int curStage = 0;
    public int stageCount => curStage;

    [Header("현재 캐릭터")]
    [SerializeField] private List<testPlayer> testPlayers = new List<testPlayer>();
    [SerializeField] private List<testMonster> testMonster = new List<testMonster>();

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
        curStage++;
    }

    #region Set
    private void SetPlayerCharacter()
    {
        // 플레이어 캐릭터 설정
    }
    #endregion

    #region Spawn
    private void SpawnPlayerCharater()
    {
        // 플레이어 캐릭터 생성하는 함수
    }

    private void SpawnEnemy()
    {
        // 적을 생성하는 함수
    }
    
    private void SpawnBoss()
    {
        // 보스 생성 함수
    }
    #endregion

}