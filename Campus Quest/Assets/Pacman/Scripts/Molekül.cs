
using UnityEngine;

public class Molekül : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().MolekuelGegessen(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zelle"))
        {
            Eat();
        }
    }
}
