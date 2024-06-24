using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreChangedEvent : UnityEvent<int> { }

public class ScoreController : MonoBehaviour
{
    public ScoreChangedEvent OnScoreChanged = new ScoreChangedEvent(); // Initialisierung des Events

    private int score;

    public int Score
    {
        get { return score; }
        private set { score = value; }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke(Score); // Auslösen des Events mit dem aktuellen Score
    }
}






