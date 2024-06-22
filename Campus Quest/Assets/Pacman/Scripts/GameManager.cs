using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool completed = false;

    public int minigameIndex;

    public Virus[] virus;

    public Zelle zelle;

    public Transform molekuel;

    public int virusMultiplier { get; private set; } = 1;

    public int score { get; private set; }

    public int leben { get; private set; }


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.leben <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
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
        ResetVirusMultiplier();

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
    }

    private void SetLeben(int leben)
    {
        this.leben = leben;
    }

    public void VirusGegessen(Virus virus)
    {
        int points = virus.points * this.virusMultiplier;
        SetScore(this.score + points);

        this.virusMultiplier++;
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

        SetScore(this.score + molekuel.points);

        if (!RemainingMolekuele())
        {
            this.zelle.gameObject.SetActive(false);
            completed = true;
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

        Invoke(nameof(ResetVirusMultiplier), molekuel.dauer);
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

    private void ResetVirusMultiplier()
    {
        this.virusMultiplier = 1;
    }
}
