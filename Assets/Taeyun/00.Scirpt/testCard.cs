using System;
using TMPro;
using UnityEngine;

public class testCard : MonoBehaviour
{
    [SerializeField] private string cardName;
    public string CardName => cardName;

    [SerializeField] private CardType cardType;
    public CardType CardType => cardType;

    [SerializeField] private int characterIndex;
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private int skillLevel;

    public void DeleteCard()
    {
        CardManager.Instance.RemoveCard(gameObject);
    }
}