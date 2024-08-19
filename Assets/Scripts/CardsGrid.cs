
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class CardsGrid : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardSlot cardSlotPrefab;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 2;

    private CardSlot[] _cardSlots;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        CalculateCellSize();
        PopulateWithCardSlots();
        PopulateSlotsWith(GenerateCards());
        
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
        var slotsCount = rows * columns;
        _cardSlots = new CardSlot[slotsCount];
        for (int i = 0; i < slotsCount; i++)
        {
            _cardSlots[i] = Instantiate(cardSlotPrefab, grid.transform);
        }
    }

    private List<Card> GenerateCards()
    {
        List<Card> _cards = new List<Card>();
        var slotsCount = _cardSlots.Length;
        var cardsSprites = GenerateSprites(slotsCount);
        for (int i = 0; i < slotsCount; i++)
        {
            if(slotsCount%2 != 0 && i==slotsCount/2) ++i;
            var newCard = Instantiate<Card>(cardPrefab);
            newCard.SetImage(GetSpriteFrom(cardsSprites));
            newCard.Index = i;
            _cards.Add(newCard);
        }

        return _cards;
    }

    private Sprite GetSpriteFrom(Dictionary<Sprite, int> sprites)
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
            _cardSlots[card.Index].PopulateWith(card);
        }
    }

    private Dictionary<Sprite, int> GenerateSprites(int cardsCount)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("sheet_white2x");
        var spritesCount = cardsCount / 2;
        var cardsSprites = new Dictionary<Sprite, int>();
        for (int i = 0; i < spritesCount; i++)
        {
            cardsSprites.Add(sprites[i],2);
        }

        return cardsSprites;
    }
}
