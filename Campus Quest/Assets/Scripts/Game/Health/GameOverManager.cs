using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen; // Referenz zum Game Over Screen

    private void Start()
    {
        gameOverScreen.SetActive(false); // Sicherstellen, dass der Game Over Screen am Anfang deaktiviert ist
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); // Aktivieren des Game Over Screens
        Time.timeScale = 0; // Spiel anhalten
    }
}

