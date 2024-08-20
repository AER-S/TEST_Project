using Common;
using UnityEngine;

public class PanelsManager : Singleton<PanelsManager>
{
    [SerializeField] private RectTransform mainMenuPanel;
    [SerializeField] private RectTransform gamePanel;
    [SerializeField] private RectTransform gameOverPanel;

    private void OnEnable()
    {
        GameManager.GameEnded += ShowGameOverPanel;
        GameManager.GameStarted += ShowGamePanel;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= ShowGameOverPanel;
        GameManager.GameStarted -= ShowGamePanel;
    }

    private void HideAll()
    {
        gamePanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void ShowMainMenuPanel()
    {
        HideAll();
        mainMenuPanel.gameObject.SetActive(true);
    }
    
    private void ShowGamePanel()
    {
        HideAll();
        gamePanel.gameObject.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        HideAll();
        gameOverPanel.gameObject.SetActive(true);
    }
}
