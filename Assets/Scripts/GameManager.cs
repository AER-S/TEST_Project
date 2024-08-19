
using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<Card> _cardsPair;

    private Queue<List<Card>> _comparisonTargets;

    private void Start()
    {
        _cardsPair = new List<Card>();
        _comparisonTargets = new Queue<List<Card>>();
        Canvas.ForceUpdateCanvases();
        CardsGrid.Instance.PopulateNewGrid();
    }

    private void Update()
    {
        while (_comparisonTargets.Count>0)
        {
            var comparisonTarget = _comparisonTargets.Dequeue();
            if (comparisonTarget[0].Value == comparisonTarget[1].Value)
            {
                Destroy(comparisonTarget[0].gameObject);
                Destroy(comparisonTarget[1].gameObject);
            }
            else
            {
                comparisonTarget[0].UnFlip();
                comparisonTarget[1].UnFlip();
            }
        }
    }

    public void TakeCard(Card card)
    {
        _cardsPair.Add(card);
        if (_cardsPair.Count >= 2)
        {
            _comparisonTargets.Enqueue(_cardsPair);
            _cardsPair = new List<Card>();
        }
    }
    
    
    
    
}
