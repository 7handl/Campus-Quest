
using UnityEngine;

public class VirusScatter : VirusBehavior
{
    private void OnDisable()
    {
        this.virus.chase.Enable();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.virus.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.virus.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.virus.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
