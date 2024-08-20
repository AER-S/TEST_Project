
using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float comparisonTime = 1;
    [SerializeField] private float showingCardsTime = 3;
    
    private List<Card> _cardsPair;

    private Queue<List<Card>> _comparisonTargets;

    private void OnEnable()
    {
        CardsGrid.AllCardsDestroyed += OnAllCardsDestroyed;
    }

    private void OnDisable()
    {
        CardsGrid.AllCardsDestroyed -= OnAllCardsDestroyed;
    }

    private void OnAllCardsDestroyed()
    {
        PanelsManager.Instance.ShowGameOverPanel();
    }

    public void Start()
    {
        _cardsPair = new List<Card>();
        _comparisonTargets = new Queue<List<Card>>();
        Canvas.ForceUpdateCanvases();
        CardsGrid.Instance.PopulateNewGrid();
        StartCoroutine(StartGame());
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
            CardsGrid.Instance.DestroyCard(pairOfCards[0]);
            CardsGrid.Instance.DestroyCard(pairOfCards[1]);
        }
        else
        {
            pairOfCards[0].UnFlip();
            pairOfCards[1].UnFlip();
        }
    }

    private IEnumerator StartGame()
    {
        CardsGrid.Instance.SetInteractable(false);
        CardsGrid.Instance.FlipAllCards();
        yield return new WaitForSecondsRealtime(showingCardsTime);
        CardsGrid.Instance.UnFlipAllCards();
        CardsGrid.Instance.SetInteractable(true);
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
