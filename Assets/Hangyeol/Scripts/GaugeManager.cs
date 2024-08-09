using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GaugeManager : MonoBehaviour
{
    [SerializeField]
    private Image gaugeItemPrefab; // 게이지 아이템 프리팹

    private List<Transform> m_objectList = new List<Transform>();
    private Dictionary<Transform, List<RectTransform>> m_gaugeDict = new Dictionary<Transform, List<RectTransform>>();

    private Camera m_cam = null;

    [Header("Gauge Settings")]
    public int maxGauge = 5; // 최대 게이지 수
    public Color defaultColor = Color.white;
    public Color filledColor = Color.red;

    private void Start()
    {
        m_cam = Camera.main;

        // "Player" 태그를 가진 객체들 추가
        AddObjectsWithTag("Player");

        // "Enemy" 태그를 가진 객체들 추가
        AddObjectsWithTag("Enemy");
    }

    private void Update()
    {
        foreach (var pair in m_gaugeDict)
        {
            Transform obj = pair.Key;
            List<RectTransform> gaugeList = pair.Value;
            Character character = obj.GetComponent<Character>();

            if (character != null)
            {
                // 캐릭터 HP가 0 이하일 경우 게이지 제거
                if (character.curHp <= 0)
                {
                    foreach (RectTransform gauge in gaugeList)
                    {
                        Destroy(gauge.gameObject); // 게이지 제거
                    }
                    m_gaugeDict.Remove(obj); // 딕셔너리에서 캐릭터 제거
                    continue;
                }

                // 게이지 위치 업데이트
                UpdateGaugePosition(obj, gaugeList);

                // 게이지 값 업데이트
                UpdateGaugeUI(character, gaugeList);
            }
        }
    }

    private void AddObjectsWithTag(string tag)
    {
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in t_objects)
        {
            Transform objTransform = go.transform;
            m_objectList.Add(objTransform);

            // 각 캐릭터에 대한 게이지 UI 생성
            List<RectTransform> gaugeList = CreateGaugeUI();
            m_gaugeDict.Add(objTransform, gaugeList);
        }
    }

    private List<RectTransform> CreateGaugeUI()
    {
        List<RectTransform> gaugeList = new List<RectTransform>();
        for (int i = 0; i < maxGauge; i++)
        {
            Image gaugeItem = Instantiate(gaugeItemPrefab, transform); // 캔버스 하위에 생성
            gaugeItem.enabled = true; // 초기에는 활성화
            gaugeList.Add(gaugeItem.rectTransform);
        }
        return gaugeList;
    }

    private void UpdateGaugePosition(Transform characterTransform, List<RectTransform> gaugeList)
    {
        Vector3 screenPos = m_cam.WorldToScreenPoint(characterTransform.position + new Vector3(0, 2.0f, 0));
        for (int i = 0; i < maxGauge; i++)
        {
            gaugeList[i].position = screenPos + new Vector3(i * 30, 0, 0); // 위치 조정
        }
    }

    private void UpdateGaugeUI(Character character, List<RectTransform> gaugeList)
    {
        int currentGauge = Mathf.CeilToInt((character.curHp / character.maxHp) * maxGauge);

        for (int i = 0; i < maxGauge; i++)
        {
            Image gaugeImage = gaugeList[i].GetComponent<Image>();
            gaugeImage.color = i < currentGauge ? filledColor : defaultColor;
            gaugeList[i].gameObject.SetActive(true); // 게이지 아이템을 활성화
        }
    }

    // 게이지를 증가시키는 메서드
    public void IncreaseGauge()
    {
        // 게이지를 증가시키는 로직을 여기에 추가할 수 있습니다.
    }
}
