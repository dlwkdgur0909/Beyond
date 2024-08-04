using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HpBarManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_goPrefabs = null;

    List<Transform> m_objeectList = new List<Transform>();
    List<GameObject> m_hpBarList = new List<GameObject>();

    Camera m_cam = null;

    private void Start()
    {
        m_cam = Camera.main;

        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < t_objects.Length; i++)
        {
            m_objeectList.Add(t_objects[i].transform);
            GameObject t_hpbar = Instantiate(m_goPrefabs, t_objects[i].transform.position, Quaternion.identity, transform);
            m_hpBarList.Add(t_hpbar);
        }
    }
    private void Update()
    {
        for (int i = 0; i < m_objeectList.Count; i++)
        {
            m_hpBarList[i].transform.position = m_cam.WorldToScreenPoint(m_objeectList[i].position + new Vector3(0, 1.15f, 0));
        }
    }
}
