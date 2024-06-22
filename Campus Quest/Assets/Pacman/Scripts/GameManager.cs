using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class GameManager : MonoBehaviour
{

    public Virus[] virus;

    public Zelle zelle;

    public Transform molekuel;

    public int score { get; private set; }

    public int leben {  get; private set; }

    public TextMeshProUGUI lebenText;

    public TextMeshProUGUI scoreText;


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.leben <= 0)
        {
            Invoke(nameof(NewGame), 2.0f);
        }
    }

    private void NewGame()
    {
        SetScore(244);
        SetLeben(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform molekuel in this.molekuel)
        {
            molekuel.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {

        for (int i = 0; i < this.virus.Length; i++)
        {
            this.virus[i].ResetState();
        }

        this.zelle.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.virus.Length; i++)
        {
            this.virus[i].gameObject.SetActive(false);
        }

        this.zelle.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLeben(int leben)
    {
        this.leben = leben;
        lebenText.text = leben.ToString();
    }

    public void VirusGegessen(Virus virus)
    {
       
    }

    public void ZelleGegessen()
    {
        this.zelle.gameObject.SetActive(false);

        SetLeben(this.leben - 1);

        if (this.leben > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }else
        {
            GameOver();
        }
    }

    public void MolekuelGegessen(Molekül molekuel)
    {
        molekuel.gameObject.SetActive(false);

        SetScore(this.score-1);

        if (!RemainingMolekuele())
        {
            this.zelle.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("Dialog");
        }
    }

    public void MedizinGegessen(Medizin molekuel)
    {
        for (int i = 0; i < this.virus.Length; i++) 
        {
            this.virus[i].frightened.Enable(molekuel.dauer);
        }
        
        MolekuelGegessen(molekuel);

        CancelInvoke();

        
    }

    private bool RemainingMolekuele()
    {
        foreach (Transform molekuel in this.molekuel)
        {
            if (molekuel.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

}
