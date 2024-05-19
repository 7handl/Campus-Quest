
using UnityEngine;

[RequireComponent(typeof(Virus))]

public abstract class VirusBehavior : MonoBehaviour
{
    public Virus virus {  get; private set; }

    public float dauer;

    private void Awake()
    {
        this.virus = GetComponent<Virus>();

        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.dauer);
    }

    public virtual void Enable(float dauer)
    {
        this.enabled = true;

        CancelInvoke();
        
        Invoke(nameof(Disable), dauer);

    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}
