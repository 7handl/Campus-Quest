using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SchummelnScript : MonoBehaviour
{

    [Header("Game UI")]
    [SerializeField] private GameObject timePanel;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoText;

    private bool isGameActive;
    private int pressCnt = 0;
    public int winPressCnt = 50;
    public KeyCode keyToPress = KeyCode.Space;
    private bool inGame;
    private bool inLostCutscene = false;
    private bool ignoreInput = false;
    public ProgressBar progressBar;


    private int deathCnt = 0;
    
    public int timeLimit = 60;
    private float timeRemaining;

    private bool teacherState;
    public float fixedTime;
    private float rndTime;
    public float rndRangeLow;
    public float rndRangeUp;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        inGame = false;
        infoText.text = "Drücke Leertaste um das Minigame zu starten";
        timeRemaining = timeLimit;
        progressBar.SetMaxProgress(winPressCnt);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;

        if (inGame && timeRemaining <= 0)
        {
            StartCoroutine(GameLost("Die Prüfungszeit ist abgelaufen!"));
        }

        if (deathCnt <= 3)
        {
            EndSequence();
        }

        if (!inGame && !ignoreInput && Input.GetKeyDown(keyToPress))
        {
            if (inLostCutscene)
            {
                StartCoroutine(RestartGame());
            }
            else { StartCoroutine(StartMinigameSchummeln()); } }

        if (inGame)
        {
            timeRemaining -= Time.deltaTime;

        }

        if (inGame && Input.GetKeyDown(keyToPress))
        {
            if (teacherState) {
                StartCoroutine(GameLost("Du wurdest erwischt!"));
            }

            if (!teacherState)
            {
                pressCnt++;
                progressBar.SetProgress(pressCnt);
                Debug.Log("Press Count:" + pressCnt);
            }

        }




        UpdateUI();
    }


    private void UpdateUI()
    {
        timeText.text = Mathf.CeilToInt(timeRemaining).ToString();

    }



    private IEnumerator StartMinigameSchummeln()
    {
        yield return new WaitForSeconds(0.5f);
        infoText.text = "Minigame läuft";
        inGame = true;
        StartCoroutine(SwitchRoutine());

    }

    private IEnumerator SwitchRoutine()
    {
        while (inGame)
        {
            rndTime = Random.Range(rndRangeLow, rndRangeUp);
            yield return new WaitForSeconds(rndTime);

            teacherState = false;
            Debug.Log("Switched to false (Lehrer Schaut nicht)");

            yield return new WaitForSeconds(fixedTime);

            teacherState = true;
            Debug.Log("Switched to true (lehrer schaut)");
        }
    }

    private IEnumerator GameLost(string message)
    {
        ignoreInput = true;
        inLostCutscene = true;
        inGame = false;
        deathCnt++;
        infoText.text = message;
        yield return new WaitForSeconds(4f);
        infoText.text = "Drücke Leertaste um das Minigame zurückzusetzen";
        ignoreInput = false;
        
    }

    

    private IEnumerator RestartGame()
    {
        timeRemaining = timeLimit;
        pressCnt = 0;
        teacherState = false;
        infoText.text = "Drücke Leertaste um das Minigame erneut zu versuchen";
        yield return new WaitForSeconds(4f);
        inLostCutscene = false;

    }
    private void GameWon()
    {

    }
    private void EndSequence()
    {
        // Sequence wenn man 3mal verloren hat
    }
}
