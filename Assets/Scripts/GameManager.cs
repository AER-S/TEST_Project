
using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float comparisonTime = 1;
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
            StartCoroutine(nameof(CompareCards), comparisonTarget);
        }
    }

    private IEnumerator CompareCards(List<Card> pairOfCards)
    {
        yield return new WaitForSecondsRealtime(comparisonTime);
        if (pairOfCards[0].Value == pairOfCards[1].Value)
        {
            Destroy(pairOfCards[0].gameObject);
            Destroy(pairOfCards[1].gameObject);
        }
        else
        {
            pairOfCards[0].UnFlip();
            pairOfCards[1].UnFlip();
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
