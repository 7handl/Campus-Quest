
using UnityEngine;

public class VirusChase : VirusBehavior
{
    private void OnDisable()
    {
        this.virus.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.virus.frightened.enabled)
        {
            
        }
    }
}
