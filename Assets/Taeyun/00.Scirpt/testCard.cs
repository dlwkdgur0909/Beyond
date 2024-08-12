using System;
using UnityEngine;

public class testCard : MonoBehaviour
{
    [SerializeField] private string cardName;
    public string CardName => cardName;

    [SerializeField] private CardType cardType;
    public CardType CardType => cardType;

    public void DeleteCard()
    {
        CardManager.Instance.RemoveCard(gameObject);
    }
}