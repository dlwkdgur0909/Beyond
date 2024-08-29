using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class testCard : MonoBehaviour
{
    [SerializeField] private string cardName;
    public string CardName => cardName;

    [SerializeField] private CardType cardType;
    public CardType CardType => cardType;


    [Header("CardUI")]
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardLevelText;
    [SerializeField] private float fontSize;

    [Header("CardInfo")]
    [SerializeField] private int characterIndex;
    public int CharacterIndex => characterIndex;

    [SerializeField] private int skillLevel;
    public int SkillLevel => skillLevel;

    private void OnEnable()
    {
        cardLevelText.fontSize = fontSize;    

        cardNameText.text = "카드 번호 : " + characterIndex.ToString();
        cardLevelText.text = "카드 레벨 : " + skillLevel.ToString() + "카드 타입 : " + cardType;
    }

    public void DeleteCard()
    {
        CardManager.Instance.RemoveCard(gameObject);
    }

    public (bool, int, int, CardType) ReturnCardInfo()
    {
        return (skillLevel != 3,characterIndex, skillLevel, cardType);
    }

    public void UseSkill()
    {
        StageManager.Instance.Players[characterIndex - 1].Attack();
    }

    public void SkillCardLevelUp()
    {
        if(skillLevel != 3)
        {
            skillLevel++;
            cardLevelText.text = "카드 레벨 : " + skillLevel.ToString() + "카드 타입 : " + cardType;
        }
    }
}