
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Virus virus;

    public Zelle zelle;

    public Transform molekuel;

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
        setScore(0);
        setLeben(3);
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
            this.virus[i].gameObject.SetActive(true);
        }

        this.zelle.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < this.virus.Length; i++)
        {
            this.virus[i].gameObject.SetActive(false);
        }

        this.zelle.gameObject.SetActive(false);
    }

    private void setScore(int score)
    {
        this.score = score;
    }

    private void setLeben(int leben)
    {
        this.leben = leben;
    }

    public void VirusGegessen(Virus virus)
    {
        setScore(this.score + virus.points);
    }

    public void ZelleGegessen()
    {
        this.zelle.gameObject.SetActive(false);

        setLeben(this.leben - 1);

        if (this.leben > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }else
        {
            GameOver();
        }
    }
}
