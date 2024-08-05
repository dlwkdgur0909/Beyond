using UnityEngine;
using System;
using System.Collections.Generic;

// CharacterData 클래스 정의
[System.Serializable]
public class CharacterData
{
    public string name;
    public float weight; // CharacterData 객체 내부에 가중치를 정의합니다.
    // 추가적인 필드들을 여기에 정의할 수 있습니다.

    public CharacterData(string name, float weight)
    {
        this.name = name;
        this.weight = weight;
    }
}

// WeightedItem 클래스 정의
[System.Serializable]
public class WeightedItem<T>
{
    public T item;
    public float weight;
}

public class WeightedRandomUtility
{
    public static T GetWeightedRandom<T>(List<WeightedItem<T>> weightedItems)
    {
        // 만약 항목이 없으면 기본값 반환
        if (weightedItems.Count == 0)
            return default(T);

        // 모든 가중치를 더하여 총합을 구함
        float totalWeight = 0f;
        foreach (var weightedItem in weightedItems)
        {
            totalWeight += weightedItem.weight;
        }

        // 0부터 총합 사이의 랜덤 값을 생성
        float randomValue = UnityEngine.Random.value * totalWeight;

        // 랜덤 값이 어느 범위에 속하는지 확인하여 항목 선택
        foreach (var weightedItem in weightedItems)
        {
            randomValue -= weightedItem.weight;
            if (randomValue <= 0)
                return weightedItem.item;
        }

        // 여기까지 왔다면 뭔가 잘못된 것이 아니므로 마지막 항목 반환
        return weightedItems[weightedItems.Count - 1].item;
    }
}
// 익스텐션 메서드 정의
public static class WeightedItemExtensions
{
    // 제네릭으로 작성된 익스텐션 메서드
    public static List<WeightedItem<T>> ToWeightedItemList<T>(this List<T> dataList, Func<T, float> weightSelector)
    {
        List<WeightedItem<T>> weightedItemList = new List<WeightedItem<T>>();
        foreach (T data in dataList)
        {
            weightedItemList.Add(new WeightedItem<T> { item = data, weight = weightSelector(data) });
        }
        return weightedItemList;
    }
}

public class Draw : MonoBehaviour
{
    [SerializeField] private GameObject tenDrawScreen;

    void Start()
    {
        // CharacterData 객체를 담는 리스트 생성
        List<CharacterData> characterList = new List<CharacterData>
        {
            new CharacterData("Alice", 0.4f),
            new CharacterData("Bob", 0.3f),
            new CharacterData("Charlie", 0.3f)
        };

        // CharacterData 리스트를 WeightedItem 리스트로 변환
        List<WeightedItem<CharacterData>> weightedCharacterList = characterList.ToWeightedItemList(character => character.weight);

        // 가중치에 따라 무작위로 CharacterData 객체를 선택합니다.
        CharacterData selectedCharacter = WeightedRandomUtility.GetWeightedRandom(weightedCharacterList);

        // 선택된 CharacterData 객체 정보 출력
        Debug.Log("Selected Character: " + selectedCharacter.name);
    }

    public void TenDraw()
    {
        tenDrawScreen.SetActive(true);
    }
}
