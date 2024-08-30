using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRandomItem : MonoBehaviour
{
    public Button[] buttonPrefabs;  // 여러 개의 버튼 프리팹 배열
    public RectTransform[] spawnPositions;  // 버튼이 생성될 3개의 위치

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<Button> availableButtons = new List<Button>(buttonPrefabs);

            foreach (RectTransform spawnPosition in spawnPositions)
            {
                // 랜덤하게 사용되지 않은 버튼 프리팹 선택
                int randomIndex = Random.Range(0, availableButtons.Count);
                Button randomButtonPrefab = availableButtons[randomIndex];

                // 선택된 버튼 프리팹을 리스트에서 제거하여 중복 생성 방지
                availableButtons.RemoveAt(randomIndex);

                // 버튼 생성
                Button newButton = Instantiate(randomButtonPrefab);

                // 생성된 버튼을 Canvas의 자식으로 설정하여 UI 요소로 추가
                newButton.transform.SetParent(spawnPosition.transform, true);

                // 버튼의 RectTransform을 가져와서 위치를 설정
                RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();

                // 버튼을 spawnPosition과 동일한 위치로 이동
                buttonRectTransform.position = spawnPosition.position;
                buttonRectTransform.localRotation = spawnPosition.localRotation;
                buttonRectTransform.localScale = spawnPosition.localScale;

                // anchoredPosition을 0으로 설정하여 정확한 위치 지정
                buttonRectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
}
