using System.Collections;
using UnityEngine;

public class VirusHome : VirusBehavior
{
    public Transform inside;

    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.virus.movement.SetDirection(-this.virus.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.virus.movement.SetDirection(Vector2.up, true);

        this.virus.movement.rigidbody.isKinematic = true;

        this.virus.movement.enabled = false;

        Vector3 position = this.transform.position;

        float dauer = 0.5f;

        float elapsed = 0.0f;

        while (elapsed < dauer)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / dauer);

            newPosition.z = position.z;
            
            this.virus.transform.position = newPosition;

            elapsed += Time.deltaTime;

            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < dauer)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / dauer);

            newPosition.z = position.z;

            this.virus.transform.position = newPosition;

            elapsed += Time.deltaTime;

            yield return null;
        }

        this.virus.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);

        this.virus.movement.rigidbody.isKinematic = false;

        this.virus.movement.enabled = true;
    }
}
