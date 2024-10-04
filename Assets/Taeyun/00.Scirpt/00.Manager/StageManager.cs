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

    [Header("스테이지")]
    [SerializeField] private int curStage = 0;
    public int CurStage => curStage;
    [SerializeField] private TextMeshProUGUI stageText;

    [Header("현재 엔티티")]
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
            //stageText.text = "현재 스테이지 : " + curStage.ToString();
        }
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

    public void EnemyDie(GameObject enemyObj)
    {
        Debug.Log("몬스터 사망");
        Destroy(enemyObj);  // 전달된 enemyObj를 파괴
        testMonster.Remove(enemyObj.GetComponent<TestEnemy>());
    }

    #endregion

    #region Spawn
    private void SpawnPlayerCharater()
    {
        // 플레이어 캐릭터 생성하는 함수
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
        // 보스 생성 함수
    }
    #endregion
}