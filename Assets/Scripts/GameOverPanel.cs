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
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
