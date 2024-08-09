using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HpBarManager : MonoBehaviour
{
    [SerializeField]
    private Slider m_goPrefabs = null; // ���������� ������ HP �� �����̴�

    private List<Transform> m_objectList = new List<Transform>();
    private List<Slider> m_hpBarList = new List<Slider>();

    private Camera m_cam = null;

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
        for (int i = 0; i < m_objectList.Count; i++)
        {
            Character character = m_objectList[i].GetComponent<Character>();
            if (character != null)
            {
                // HP�� 0 ������ ��� ü�� �� ����
                if (character.curHp <= 0)
                {
                    Destroy(m_hpBarList[i].gameObject); // HP �� ����
                    m_objectList.RemoveAt(i); // ����Ʈ���� ĳ���� ����
                    m_hpBarList.RemoveAt(i); // ����Ʈ���� HP �� ����
                    i--; // �ε����� �ϳ� ���ҽ��� ����Ʈ ũ�� ����
                    continue;
                }

                // HP �� ��ġ ������Ʈ
                m_hpBarList[i].transform.position = m_cam.WorldToScreenPoint(m_objectList[i].position + new Vector3(0, 2, 0));

                // HP �� ������Ʈ
                m_hpBarList[i].value = character.curHp / character.maxHp;
            }
        }
    }

    private void AddObjectsWithTag(string tag)
    {
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < t_objects.Length; i++)
        {
            m_objectList.Add(t_objects[i].transform);
            Slider t_hpbar = Instantiate(m_goPrefabs, transform); // �θ� ĵ������ ����
            m_hpBarList.Add(t_hpbar);
        }
    }
}
