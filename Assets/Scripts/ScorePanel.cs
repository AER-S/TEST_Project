using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;

    [SerializeField] private TMP_Text movesDisplay;

    [SerializeField] private TMP_Text comboDisplay;

    private void OnEnable()
    {
        ScoreSystem.ScoreUpdated += OnScoreUpdate;
    }

    private void OnDisable()
    {
        ScoreSystem.ScoreUpdated -= OnScoreUpdate;
    }

    private void OnScoreUpdate()
    {
        scoreDisplay.text = ScoreSystem.Instance.Score.ToString();
        movesDisplay.text = ScoreSystem.Instance.Moves.ToString();
        comboDisplay.text = ScoreSystem.Instance.Combo.ToString();
    }
    
}
