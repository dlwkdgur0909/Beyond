using TMPro;
using UnityEngine;
using UnityEngine.UI;  // UI Button�� ���� �߰�

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

    private Button cardButton;  // ��ư ������Ʈ

    private void Awake()
    {
        cardButton = GetComponent<Button>();  // ��ư ������Ʈ ��������
        cardButton.onClick.AddListener(OnCardClick);  // ��ư Ŭ�� �� ī�� ����
    }

    private void OnEnable()
    {
        cardLevelText.fontSize = fontSize;

        cardNameText.text = "ī�� ��ȣ : " + characterIndex.ToString();
        cardLevelText.text = $"ī�� ���� : {skillLevel} " + cardType.ToString();
    }

    // ��ư Ŭ�� �� ī�� ���� ���� ����
    private void OnCardClick()
    {
        CardManager.Instance.SelectCard(this);  // ī�� ����
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
        StageManager.Instance.Players[characterIndex - 1].Attack();  // ���÷� ���� ����
    }

    public void SkillCardLevelUp()
    {
        if (skillLevel != 3)
        {
            skillLevel++;
            cardLevelText.text = $"ī�� ���� : {skillLevel} " + cardType.ToString();
        }
    }
}
