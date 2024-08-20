using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PanelsManager : Singleton<PanelsManager>
{
    [SerializeField] private RectTransform mainMenuPanel;
    [SerializeField] private RectTransform gamePanel;
    [SerializeField] private RectTransform gameOverPanel;

    private void HideAll()
    {
        gamePanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        //mainMenuPanel.gameObject.SetActive(false);
    }

    public void ShowMainMenuPanel()
    {
        HideAll();
        mainMenuPanel.gameObject.SetActive(true);
    }
    
    public void ShowGamePanel()
    {
        HideAll();
        gamePanel.gameObject.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        HideAll();
        gameOverPanel.gameObject.SetActive(true);
    }
}
