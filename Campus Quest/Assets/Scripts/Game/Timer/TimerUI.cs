using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public float timeLimit = 360f; // 6 Minuten in Sekunden
    private float timeRemaining;
    private bool timerIsRunning = false;

    public TMP_Text timerText; // Referenz zum TextMeshPro-Textobjekt

    void Start()
    {
        timeRemaining = timeLimit;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);
            }
            else
            {
                Debug.Log("Zeit abgelaufen!");
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerUI(timeRemaining);
            }
        }
    }

    void UpdateTimerUI(float timeToDisplay)
    {
        timeToDisplay += 1; // Für eine präzisere Anzeige

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}






