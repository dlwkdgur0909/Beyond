using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GaugeManager : MonoBehaviour
{
    [SerializeField]
    private Image gaugeItemPrefab; // ������ ������ ������

    private List<Transform> m_objectList = new List<Transform>();
    private Dictionary<Transform, List<RectTransform>> m_gaugeDict = new Dictionary<Transform, List<RectTransform>>();

    private Camera m_cam = null;

    [Header("Gauge Settings")]
    public int maxGauge = 5; // �ִ� ������ ��
    public Color defaultColor = Color.white;
    public Color filledColor = Color.red;

    private void Start()
    {
        m_cam = Camera.main;

        // "Player" �±׸� ���� ��ü�� �߰�
        AddObjectsWithTag("Player");

        // "Enemy" �±׸� ���� ��ü�� �߰�
        AddObjectsWithTag("Enemy");
    }

    private void Update()
    {
        List<Transform> keysToRemove = new List<Transform>();

        foreach (var pair in m_gaugeDict)
        {
            Transform obj = pair.Key;
            List<RectTransform> gaugeList = pair.Value;
            Character character = obj.GetComponent<Character>();

            if (character != null)
            {
                // ĳ���� HP�� 0 ������ ��� ������ ����
                if (character.curHp <= 0)
                {
                    foreach (RectTransform gauge in gaugeList)
                    {
                        Destroy(gauge.gameObject); // ������ ����
                    }
                    keysToRemove.Add(obj); // ������ Ű�� ��Ͽ� �߰�
                }
                else
                {
                    // ������ ��ġ ������Ʈ
                    UpdateGaugePosition(obj, gaugeList);

                    // ������ �� ������Ʈ
                    UpdateGaugeUI(character, gaugeList);
                }
            }
        }

        // ������ ���� �� ������ Ű���� �Ѳ����� ����
        foreach (Transform key in keysToRemove)
        {
            m_gaugeDict.Remove(key);
        }
    }

    private void AddObjectsWithTag(string tag)
    {
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in t_objects)
        {
            Transform objTransform = go.transform;
            m_objectList.Add(objTransform);

            // �� ĳ���Ϳ� ���� ������ UI ����
            List<RectTransform> gaugeList = CreateGaugeUI();
            m_gaugeDict.Add(objTransform, gaugeList);
        }
    }

    private List<RectTransform> CreateGaugeUI()
    {
        List<RectTransform> gaugeList = new List<RectTransform>();
        for (int i = 0; i < maxGauge; i++)
        {
            Image gaugeItem = Instantiate(gaugeItemPrefab, transform); // ĵ���� ������ ����
            gaugeItem.color = defaultColor; // �ʱ� ���� ����
            gaugeItem.enabled = true; // �ʱ⿡�� Ȱ��ȭ
            gaugeList.Add(gaugeItem.rectTransform);
        }
        return gaugeList;
    }

    private void UpdateGaugePosition(Transform characterTransform, List<RectTransform> gaugeList)
    {
        Vector3 screenPos = m_cam.WorldToScreenPoint(characterTransform.position + new Vector3(-1.5f, 1.5f, 0));
        for (int i = 0; i < maxGauge; i++)
        {
            gaugeList[i].position = screenPos + new Vector3(i * 30, 0, 0); // ��ġ ����
        }
    }

    private void UpdateGaugeUI(Character character, List<RectTransform> gaugeList)
    {
        for (int i = 0; i < maxGauge; i++)
        {
            Image gaugeImage = gaugeList[i].GetComponent<Image>();
            gaugeImage.color = i < character.gauge ? filledColor : defaultColor;
        }
    }

    // �������� ������Ű�� �޼���
    public void IncreaseGauge(Transform characterTransform)
    {
        if (m_gaugeDict.ContainsKey(characterTransform))
        {
            Character character = characterTransform.GetComponent<Character>();

            if (character != null && character.gauge < maxGauge)
            {
                character.gauge++;
                UpdateGaugeUI(character, m_gaugeDict[characterTransform]);
            }
        }
    }
    
    // �������� �ʱ�ȭ��Ű�� �޼���
    public void ResetGauge(Transform characterTransform)
    {
        if (m_gaugeDict.ContainsKey(characterTransform))
        {
            Character character = characterTransform.GetComponent<Character>();
            character.gauge = 0; // ĳ������ ������ �ʱ�ȭ
            UpdateGaugeUI(character, m_gaugeDict[characterTransform]);
        }
    }
}
