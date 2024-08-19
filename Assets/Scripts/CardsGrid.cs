
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class CardsGrid : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 2;

    

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        CalculateCellSize();
        var cardsCount = rows * columns;
        Sprite[] sprites = Resources.LoadAll<Sprite>("sheet_white2x");
        for (int i = 0; i < cardsCount; i++)
        {
            var newCard = Instantiate<Card>(cardPrefab,grid.transform);
            newCard.SetImage(sprites[i]);
        }
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
}
