using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("��������")]
    [SerializeField] private int curStage = 0;
    public int stageCount => curStage;

    [Header("���� ĳ����")]
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
        // �÷��̾� ĳ���� ����
    }
    #endregion

    #region Spawn
    private void SpawnPlayerCharater()
    {
        // �÷��̾� ĳ���� �����ϴ� �Լ�
    }

    private void SpawnEnemy()
    {
        // ���� �����ϴ� �Լ�
    }
    
    private void SpawnBoss()
    {
        // ���� ���� �Լ�
    }
    #endregion

}