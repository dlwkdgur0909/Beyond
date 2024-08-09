using UnityEngine;
using System;
using System.Collections.Generic;

// CharacterData Ŭ���� ����
[System.Serializable]
public class CharacterData
{
    public string name; //ī�� �̸�
    public float weight; // CharacterData ��ü ���ο� ����ġ�� �����մϴ�.
    public GameObject card; //ī��
    // �߰����� �ʵ���� ���⿡ ������ �� �ֽ��ϴ�.

    public CharacterData(string name,float weight, GameObject card)
    {
        this.name = name;
        this.weight = weight;
        this.card = card;
    }
}

// WeightedItem Ŭ���� ����
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
        // ���� �׸��� ������ �⺻�� ��ȯ
        if (weightedItems.Count == 0)
            return default(T);

        // ��� ����ġ�� ���Ͽ� ������ ����
        float totalWeight = 0f;
        foreach (var weightedItem in weightedItems)
        {
            totalWeight += weightedItem.weight;
        }

        // 0���� ���� ������ ���� ���� ����
        float randomValue = UnityEngine.Random.value * totalWeight;

        // ���� ���� ��� ������ ���ϴ��� Ȯ���Ͽ� �׸� ����
        foreach (var weightedItem in weightedItems)
        {
            randomValue -= weightedItem.weight;
            if (randomValue <= 0)
                return weightedItem.item;
        }

        // ������� �Դٸ� ���� �߸��� ���� �ƴϹǷ� ������ �׸� ��ȯ
        return weightedItems[weightedItems.Count - 1].item;
    }
}
// �ͽ��ټ� �޼��� ����
public static class WeightedItemExtensions
{
    // ���׸����� �ۼ��� �ͽ��ټ� �޼���
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
    [SerializeField] private GameObject oneDrawScreen;
    [SerializeField] private GameObject tenDrawScreen;

    public List<CharacterData> cardList = new List<CharacterData>();
    void Start()
    {
        // CharacterData ����Ʈ�� WeightedItem ����Ʈ�� ��ȯ
        List<WeightedItem<CharacterData>> weightedCharacterList = cardList.ToWeightedItemList(character => character.weight);

        // ����ġ�� ���� �������� CharacterData ��ü�� �����մϴ�.
        CharacterData selectedCharacter = WeightedRandomUtility.GetWeightedRandom(weightedCharacterList);

        // ���õ� CharacterData ��ü ���� ���
        Debug.Log(selectedCharacter.name);
    }

    public void OneDraw()
    {
        oneDrawScreen.SetActive(true);
    }
    public void TenDraw()
    {
        tenDrawScreen.SetActive(true);
    }
}
