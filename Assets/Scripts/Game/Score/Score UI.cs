using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text _scoreText;
    public ScoreController scoreController; // Referenz zum ScoreController

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (scoreController != null)
        {
            scoreController.OnScoreChanged.AddListener(UpdateScore);
        }
        else
        {
            Debug.LogError("ScoreController reference is not set in ScoreUI.");
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }
}





