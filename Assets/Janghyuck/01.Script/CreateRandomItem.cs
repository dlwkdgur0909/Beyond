using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRandomItem : MonoBehaviour
{
    public Button[] buttonPrefabs;  // ���� ���� ��ư ������ �迭
    public RectTransform[] spawnPositions;  // ��ư�� ������ 3���� ��ġ

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<Button> availableButtons = new List<Button>(buttonPrefabs);

            foreach (RectTransform spawnPosition in spawnPositions)
            {
                // �����ϰ� ������ ���� ��ư ������ ����
                int randomIndex = Random.Range(0, availableButtons.Count);
                Button randomButtonPrefab = availableButtons[randomIndex];

                // ���õ� ��ư �������� ����Ʈ���� �����Ͽ� �ߺ� ���� ����
                availableButtons.RemoveAt(randomIndex);

                // ��ư ����
                Button newButton = Instantiate(randomButtonPrefab);

                // ������ ��ư�� Canvas�� �ڽ����� �����Ͽ� UI ��ҷ� �߰�
                newButton.transform.SetParent(spawnPosition.transform, true);

                // ��ư�� RectTransform�� �����ͼ� ��ġ�� ����
                RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();

                // ��ư�� spawnPosition�� ������ ��ġ�� �̵�
                buttonRectTransform.position = spawnPosition.position;
                buttonRectTransform.localRotation = spawnPosition.localRotation;
                buttonRectTransform.localScale = spawnPosition.localScale;

                // anchoredPosition�� 0���� �����Ͽ� ��Ȯ�� ��ġ ����
                buttonRectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
}
