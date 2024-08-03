using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance
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
    private static StageManager instance;

    [Header("스테이지")]
    [SerializeField] private int curStage = 0;
    public int CurStage => curStage;

    [Header("현재 엔티티")]
    [SerializeField] private List<testPlayer> players = new List<testPlayer>();
    public List<testPlayer> Players => players;
    [SerializeField] private List<testMonster> testMonster = new List<testMonster>();

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