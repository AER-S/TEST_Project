using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void Restart()
    {
        PanelsManager.Instance.ShowGamePanel();
        GameManager.Instance.Start();
    }

    public void GoToMainMenu()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
