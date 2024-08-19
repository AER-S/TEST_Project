using Common;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        CardsGrid.Instance.PopulateNewGrid();
    }
}
