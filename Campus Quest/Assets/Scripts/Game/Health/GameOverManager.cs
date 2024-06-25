using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    [SerializeField] private GameObject standardGameOverScreen;
    [SerializeField] private GameObject specialGameOverScreen;
    [SerializeField] private Button restartButton; // Button on the standard Game Over Screen
    [SerializeField] private Button specialNewGameButton; // Button on the special Game Over Screen

    private int deathCount = 0;
    private int maxDeaths = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        restartButton.onClick.AddListener(RestartGame);
        specialNewGameButton.onClick.AddListener(StartNewGame); // Bind new method to button
    }

    private void Start()
    {
        standardGameOverScreen.SetActive(false);
        specialGameOverScreen.SetActive(false);
    }

    public void OnPlayerDeath()
    {
        deathCount++;
        if (deathCount < maxDeaths)
        {
            ShowGameOverScreen();
        }
        else
        {
            ShowSpecialGameOverScreen();
        }
    }

    private void ShowGameOverScreen()
    {
        standardGameOverScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    private void ShowSpecialGameOverScreen()
    {
        specialGameOverScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        deathCount = 0; // Reset death count on restart
    }

    private void StartNewGame()
    {
        Time.timeScale = 1; // Unpause the game
        DataPersistenceManager.instance.NewGame(); // Assuming DataPersistenceManager is set up
        SceneManager.LoadScene("Trainstation"); // Load the initial game scene or any other scene as needed
        deathCount = 0; // Reset death count
    }
}
































