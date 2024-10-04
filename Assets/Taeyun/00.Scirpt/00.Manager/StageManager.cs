using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI stageText;

    [Header("���� ��ƼƼ")]
    [SerializeField] private List<Knight> players = new List<Knight>();
    public List<Knight> Players => players;
    [SerializeField] private List<TestEnemy> testMonster = new List<TestEnemy>();
    [SerializeField] private List<GameObject> testMonsterPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> testMonsterSpawnTransforms = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NextStage();
        SpawnEnemy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            curStage++;
            //stageText.text = "���� �������� : " + curStage.ToString();
        }
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

    public void EnemyDie(GameObject enemyObj)
    {
        Debug.Log("���� ���");
        Destroy(enemyObj);  // ���޵� enemyObj�� �ı�
        testMonster.Remove(enemyObj.GetComponent<TestEnemy>());
    }

    #endregion

    #region Spawn
    private void SpawnPlayerCharater()
    {
        // �÷��̾� ĳ���� �����ϴ� �Լ�
    }

    private void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, testMonsterPrefabs.Count);
        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = Instantiate(testMonsterPrefabs[ranEnemy], testMonsterSpawnTransforms[i].position, Quaternion.identity);
            testMonster.Add(enemy.GetComponent<TestEnemy>());
        }
    }
    
    private void SpawnBoss()
    {
        // ���� ���� �Լ�
    }
    #endregion
}