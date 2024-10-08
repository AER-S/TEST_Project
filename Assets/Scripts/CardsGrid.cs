using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class CardsGrid : Common.Singleton<CardsGrid>
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardSlot cardSlotPrefab;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 2;

    public static Action AllCardsDestroyed;
    public int Rows => rows;
    public void SetRows(float value) => rows = (int)value;
    public int Columns => columns;
    public void SetColumns(float value) => columns = (int)value;

    private ObjectPool<Card> _cardsPool;

    private ObjectPool<CardSlot> _slotsPool;
    private CardSlot[] _cardSlots;
    private List<Card> _cards;

    new void Awake()
    {
        base.Awake();
        _cardsPool = new ObjectPool<Card>(cardPrefab, rows * columns);
        _slotsPool = new ObjectPool<CardSlot>(cardSlotPrefab, rows * columns);
    }

    public List<Card.CardData> GetCardsData()
    {
        List<Card.CardData> cardsData = new List<Card.CardData>();
        foreach (var card in _cards)
        {
            cardsData.Add(card.Data);
        }

        return cardsData;
    }

    private void SetCards(List<Card.CardData> cards)
    {
        _cards = new List<Card>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("sheet_white2x");
        foreach (var cardData in cards)
        {
            var newCard = _cardsPool.Get();
            newCard.SetImage(sprites[cardData.Value]);
            newCard.Data.Value = cardData.Value;
            newCard.Data.Index = cardData.Index;
            _cards.Add(newCard);
        }
    }
    


    public void Populate()
    {
        if(GameManager.Instance.IsSavedGame) PopulateSavedGrid();
        else PopulateNewGrid();
    }

    private void PopulateGrid()
    {
        CalculateCellSize();
        PopulateWithCardSlots();
        PopulateSlotsWith(_cards);
    }

    private void PopulateSavedGrid()
    {
        rows = GameManager.Instance.GameData.Rows;
        columns = GameManager.Instance.GameData.Columns;
        SetCards(GameManager.Instance.GameData.LastGameCards);
        PopulateGrid();
    }

    private void PopulateNewGrid()
    {
        _cards = GenerateCards();
        PopulateGrid();
    }

    public void DestroyCard(Card card)
    {
        _cards.Remove(card);
        card.DestroyCard();
        _cardsPool.ReturnToPool(card,true);
        if(_cards.Count<=0) AllCardsDestroyed?.Invoke();
    }

    public void FlipAllCards()
    {
        foreach (var card in _cards)
        {
            card.Flip();
        }
    }

    public void UnFlipAllCards()
    {
        foreach (var card in _cards)
        {
            card.UnFlip();
        }
    }

    private struct SpriteData
    {
        public Sprite Sprite;
        public int Value;
    }

    private void CalculateCellSize()
    {
        var gridRectTransform = grid.GetComponent<RectTransform>();
        var gridWidth = gridRectTransform.rect.width-grid.padding.left-grid.padding.right-(columns-1)*grid.spacing.x;
        Debug.Log("Width: "+ gridWidth);
        var gridHeight = gridRectTransform.rect.height-grid.padding.top-grid.padding.bottom-(rows-1)*grid.spacing.y;;
        Debug.Log("Height: "+ gridHeight);
        grid.cellSize = new Vector2(gridWidth / columns, gridHeight / rows);
    }

    private void PopulateWithCardSlots()
    {
        if(_cardSlots is { Length: > 0 }) ClearSlots();
        var slotsCount = rows * columns;
        _cardSlots = new CardSlot[slotsCount];
        for (int i = 0; i < slotsCount; i++)
        {
            _cardSlots[i] = _slotsPool.Get();
            _cardSlots[i].transform.SetParent(grid.transform);
        }
    }

    private List<Card> GenerateCards()
    {
        List<Card> cards = new List<Card>();
        var slotsCount = rows*columns;
        var cardsSprites = GenerateSprites(slotsCount);
        for (int i = 0; i < slotsCount; i++)
        {
            if(slotsCount%2 != 0 && i==slotsCount/2) ++i;
            var newCard = _cardsPool.Get();
            var spriteData = GetSpriteFrom(cardsSprites);
            newCard.SetImage(spriteData.Sprite);
            newCard.Data.Value = spriteData.Value;
            newCard.Data.Index = i;
            cards.Add(newCard);
        }

        return cards;
    }
    

    private SpriteData GetSpriteFrom(Dictionary<SpriteData, int> sprites)
    {
        var randomIndex = Random.Range(0, sprites.Count);
        var sprite = sprites.ElementAt(randomIndex);
        if (sprite.Value == 1) sprites.Remove(sprite.Key);
        else sprites[sprite.Key] = 1;
        return sprite.Key;
    }

    private void PopulateSlotsWith(List<Card> cards)
    {
        foreach (var card in cards)
        {
            _cardSlots[card.Data.Index].PopulateWith(card);
        }
    }

    private Dictionary<SpriteData, int> GenerateSprites(int cardsCount)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("sheet_white2x");
        var spritesCount = cardsCount / 2;
        var cardsSprites = new Dictionary<SpriteData, int>();
        for (int i = 0; i < spritesCount; i++)
        {
            var newSpriteData = new SpriteData(){Sprite = sprites[i], Value = i};
            cardsSprites.Add(newSpriteData,2);
        }

        return cardsSprites;
    }

    private void ClearSlots()
    {
        foreach (var slot in _cardSlots)
        {
            _slotsPool.ReturnToPool(slot);
        }

        _cardSlots = Array.Empty<CardSlot>();
    }
    
}
