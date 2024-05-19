
using UnityEngine;

public class Virus : MonoBehaviour
{
    public Movement movement {  get; private set; }

    public VirusHome home { get; private set; }

    public VirusScatter scatter { get; private set; }

    public VirusChase chase { get; private set; }

    public VirusFrightened frightened { get; private set; }

    public VirusBehavior initialBehavior;

    public Transform target;
    
    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();

        this.home = GetComponent<VirusHome>();

        this.scatter = GetComponent<VirusScatter>();

        this.chase = GetComponent<VirusChase>();

        this.frightened = GetComponent<VirusFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);

        this.movement.ResetState();

        this.frightened.Disable();

        this.chase.Disable();

        this.scatter.Enable();

        if (this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Zelle"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().VirusGegessen(this);
            } else
            {
                FindObjectOfType<GameManager>().ZelleGegessen();
            }
        }
    }
}
