using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float comparisonTime = 1;
    [SerializeField] private float showingCardsTime = 3;

    public static Action<bool> DoneComparing;
    public static Action GameEnded;
    public static Action GameStarted;

    public SaveSystem.GameData GameData { get; private set; }
    public bool IsSavedGame { get; private set; }
    
    private List<Card> _cardsPair;

    private Queue<List<Card>> _comparisonTargets;

    private bool _isPlaying;

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
        StartCoroutine(GameOver());
        _isPlaying = false;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2);
        GameEnded?.Invoke();
    }
    
    public void NewGame()
    {
        IsSavedGame = false;
    }

    public void Start()
    {
        if(IsSavedGame) StartGame();
        else
        {
            PanelsManager.Instance.ShowMainMenuPanel();
        }
    }

    public void StartGame()
    {
        GameStarted?.Invoke();
        _cardsPair = new List<Card>();
        _comparisonTargets = new Queue<List<Card>>();
        Canvas.ForceUpdateCanvases();
        ScoreSystem.Instance.Start();
        CardsGrid.Instance.Populate();
        StartCoroutine(FlipCards());
        _isPlaying = true;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            while (_comparisonTargets.Count>0)
            {
                var comparisonTarget = _comparisonTargets.Dequeue();
                StartCoroutine(nameof(CompareCards), comparisonTarget);
            }
        }
    }

    private IEnumerator CompareCards(List<Card> pairOfCards)
    {
        yield return new WaitForSecondsRealtime(comparisonTime);
        if (pairOfCards[0].Data.Value == pairOfCards[1].Data.Value)
        {
            CardsGrid.Instance.DestroyCard(pairOfCards[0]);
            CardsGrid.Instance.DestroyCard(pairOfCards[1]);
            DoneComparing.Invoke(true);
        }
        else
        {
            pairOfCards[0].UnFlip();
            pairOfCards[1].UnFlip();
            DoneComparing.Invoke(false);
        }
    }

    private IEnumerator FlipCards()
    {
        CardsGrid.Instance.FlipAllCards();
        yield return new WaitForSecondsRealtime(showingCardsTime);
        CardsGrid.Instance.UnFlipAllCards();
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
        IsSavedGame = GameData != null && GameData.LastGameCards.Count > 0;
    }

    private void OnDestroy()
    {
        CollectData();
        SaveSystem.Instance.SaveGame(GameData);
    }

    private void CollectData()
    {
        GameData ??= new SaveSystem.GameData();
        GameData.Rows = CardsGrid.Instance.Rows;
        GameData.Columns = CardsGrid.Instance.Columns;
        GameData.Score = ScoreSystem.Instance.Score;
        GameData.Moves = ScoreSystem.Instance.Moves;
        GameData.Combo = ScoreSystem.Instance.Combo;
        GameData.LastGameCards = CardsGrid.Instance.GetCardsData().Count > 0 ? CardsGrid.Instance.GetCardsData() : new List<Card.CardData>();
    }

    
}
