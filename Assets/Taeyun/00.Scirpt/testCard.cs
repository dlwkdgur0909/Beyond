using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCard : MonoBehaviour
{
    [SerializeField] private string cardName;
    public string CardName => cardName;

    [SerializeField] private CardType cardType;
    public CardType CardType => cardType;

    [SerializeField] private Action cardAbility;
    public Action CardAbility => cardAbility;

    public void DeleteCard()
    {
        CardManager.Instance.CurrentCard.Remove(this);
        Destroy(gameObject);
    }
}
