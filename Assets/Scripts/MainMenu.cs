using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text rowsDisplay;
    [SerializeField] private TMP_Text columnsDisplay;
    [SerializeField] private Slider rowsSlider;
    [SerializeField] private Slider columnsSlider;

    private void OnEnable()
    {
        rowsSlider.onValueChanged.AddListener(UpdateRowDisplay);
        columnsSlider.onValueChanged.AddListener(UpdateColumnsDisplay);
    }

    private void OnDisable()
    {
        rowsSlider.onValueChanged.RemoveListener(UpdateRowDisplay);
        columnsSlider.onValueChanged.RemoveListener(UpdateColumnsDisplay);
    }

    private void UpdateRowDisplay(float value)
    {
        rowsDisplay.text = value.ToString();
    }

    private void UpdateColumnsDisplay(float value)
    {
        columnsDisplay.text = value.ToString();
    }

    private void Start()
    {
        rowsDisplay.text = CardsGrid.Instance.Rows.ToString();
        columnsDisplay.text = CardsGrid.Instance.Columns.ToString();
        rowsSlider.value = CardsGrid.Instance.Rows;
        columnsSlider.value = CardsGrid.Instance.Columns;
    }

    public void StartGame()
    {
        CardsGrid.Instance.SetRows(rowsSlider.value);
        CardsGrid.Instance.SetColumns(columnsSlider.value);
        GameManager.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
