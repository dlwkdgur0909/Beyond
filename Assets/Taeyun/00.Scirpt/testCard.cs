using TMPro;
using UnityEngine;
using UnityEngine.UI;  // UI Button을 위해 추가

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

    private Button cardButton;  // 버튼 컴포넌트

    private void Awake()
    {
        cardButton = GetComponent<Button>();  // 버튼 컴포넌트 가져오기
        cardButton.onClick.AddListener(OnCardClick);  // 버튼 클릭 시 카드 선택
    }

    private void OnEnable()
    {
        cardLevelText.fontSize = fontSize;

        cardNameText.text = "카드 번호 : " + characterIndex.ToString();
        cardLevelText.text = $"카드 레벨 : {skillLevel} " + cardType.ToString();
    }

    // 버튼 클릭 시 카드 선택 로직 실행
    private void OnCardClick()
    {
        CardManager.Instance.SelectCard(this);  // 카드 선택
    }

    public void DeleteCard()
    {
        CardManager.Instance.RemoveCard(gameObject);
    }

    public (bool, int, int, CardType) ReturnCardInfo()
    {
        return (skillLevel != 3, characterIndex, skillLevel, cardType);
    }

    public void UseSkill()
    {
        StageManager.Instance.Players[characterIndex - 1].Attack();  // 예시로 공격 실행
    }

    public void SkillCardLevelUp()
    {
        if (skillLevel != 3)
        {
            skillLevel++;
            cardLevelText.text = $"카드 레벨 : {skillLevel} " + cardType.ToString();
        }
    }
}
