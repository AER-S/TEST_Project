
using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float comparisonTime = 1;
    [SerializeField] private float showingCardsTime = 3;
    
    public bool IsSavedGame { get; private set; }
    private List<Card> _cardsPair;

    private Queue<List<Card>> _comparisonTargets;

    
    public SaveSystem.GameData GameData { get; private set; }

    new void Awake()
    {
        base.Awake();
        GameData = SaveSystem.Instance.LoadGame();
        
    }

    private void OnEnable()
    {
        CardsGrid.AllCardsDestroyed += OnAllCardsDestroyed;
        ProcessGameData();
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
        CardsGrid.Instance.Populate();
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
        if (pairOfCards[0].Data.Value == pairOfCards[1].Data.Value)
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

    private void ProcessGameData()
    {
        IsSavedGame = false;
        if (GameData != null)
        {
            if (GameData.LastGameCards.Count > 0)
            {
                IsSavedGame = true;
            }
        }
        
    }

    private void OnDestroy()
    {
        if (GameData == null) GameData = new SaveSystem.GameData();
        GameData.Rows = CardsGrid.Instance.Rows;
        GameData.Columns = CardsGrid.Instance.Columns;
        GameData.LastGameCards = CardsGrid.Instance.Cards().Count > 0 ? CardsGrid.Instance.Cards() : new List<Card.CardData>();
        SaveSystem.Instance.SaveGame(GameData);
    }
}
