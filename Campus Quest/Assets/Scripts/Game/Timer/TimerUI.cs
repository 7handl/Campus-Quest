using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    public float timeLimit; // 6 Minuten in Sekunden
    private float timeRemaining;
    private bool timerIsRunning = false;

    public TMP_Text timerText; // Referenz zum TextMeshPro-Textobjekt
    public GameObject successScreen; // Referenz zum Erfolgsscreen

    void Start()
    {
        timeRemaining = timeLimit;
        timerIsRunning = true;
        successScreen.SetActive(false); // Erfolgsscreen zu Beginn verstecken
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
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerUI(timeRemaining);
                ShowSuccessScreen();
            }
        }
    }

    void UpdateTimerUI(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowSuccessScreen()
    {
        successScreen.SetActive(true);
        Time.timeScale = 0f; // Spielzeit anhalten
    }
}







