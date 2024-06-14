
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool miniGameCompleted = false;
    
    public Virus[] virus;

    public Zelle zelle;

    public Transform molekuel;

    public int leben { get; private set; }

    [SerializeField] private string mainSceneName = "Dialog";


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

    private void SetLeben(int leben)
    {
        this.leben = leben;
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

        if (!RemainingMolekuele())
        {
            this.zelle.gameObject.SetActive(false);

            miniGameCompleted = true;

            Invoke(nameof(ReturnToMainScene), 3.0f);
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

    private void ReturnToMainScene()
    {
        if (miniGameCompleted == true)
        {
            DataPersistenceManager.instance.SaveGame();
        }
        
        SceneManager.LoadScene(mainSceneName);

    }

}
