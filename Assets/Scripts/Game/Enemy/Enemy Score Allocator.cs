using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] private int _killScore = 10; // Standardwert auf 10 gesetzt

    private ScoreController _scoreController;

    private void Awake()
    {
        _scoreController = FindObjectOfType<ScoreController>();
    }

    public void AllocateScore()
    {
        if (_scoreController != null)
        {
            _scoreController.AddScore(_killScore);
        }
    }
}


