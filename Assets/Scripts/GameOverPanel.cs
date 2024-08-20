using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.NewGame();
        GameManager.Instance.Start();
    }

    public void GoToMainMenu()
    {
        PanelsManager.Instance.ShowMainMenuPanel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
