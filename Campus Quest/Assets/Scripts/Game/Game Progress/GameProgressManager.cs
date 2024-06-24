using UnityEngine;
using UnityEngine.Events;

public class GameProgressManager : MonoBehaviour
{
    private int completedMiniGames = 0;
    public int totalMiniGames = 3; // Anzahl der zu bew�ltigenden Minispiele

    public UnityEvent onAllMiniGamesCompleted; // Event, das ausgel�st wird, wenn alle Minispiele abgeschlossen sind

    void Start()
    {
        if (onAllMiniGamesCompleted == null)
            onAllMiniGamesCompleted = new UnityEvent();
    }

    public void CompleteMiniGame()
    {
        completedMiniGames++;
        if (completedMiniGames >= totalMiniGames)
        {
            onAllMiniGamesCompleted.Invoke(); // Erfolgsscreen anzeigen
        }
    }
}

