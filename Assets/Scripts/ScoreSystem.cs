using System;
using Common;
using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    public static Action ScoreUpdated;
    public int Score { get; private set; }
    public int Moves { get; private set; }
    public int Combo { get; private set; }

    private void OnEnable()
    {
        GameManager.DoneComparing += OnComparingDone;
    }

    private void OnDisable()
    {
        GameManager.DoneComparing -= OnComparingDone;
    }

    private void OnComparingDone(bool value)
    {
        if (value)
        {
            ++Combo;
            Score += 100 * Combo;
        }
        else
        {
            Combo = 0;
        }

        ++Moves;
        ScoreUpdated?.Invoke();
    }

    public void Start()
    {
        if (GameManager.Instance.IsSavedGame)
        {
            Score = GameManager.Instance.GameData.Score;
            Moves = GameManager.Instance.GameData.Moves;
            Combo = GameManager.Instance.GameData.Combo;
        }
        else
        {
            Score = 0;
            Moves = 0;
            Combo = 0; 
        }
        
        ScoreUpdated?.Invoke();
    }
    
    
}
