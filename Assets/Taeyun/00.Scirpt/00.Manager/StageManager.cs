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

    [Header("��������")]
    [SerializeField] private int curStage = 0;
    public int CurStage => curStage;

    [Header("���� ��ƼƼ")]
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