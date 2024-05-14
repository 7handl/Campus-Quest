
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
            Vector2 direction = Vector2.zero;

            float minDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);

                float distance = (this.virus.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;

                    minDistance = distance;
                }
            }

            this.virus.movement.SetDirection(direction);
        }
    }
}
