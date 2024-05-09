
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public Transform verbindung;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;

        position.x = this.verbindung.position.x;

        position.y = this.verbindung.position.y;

        other.transform.position = position;
    }
}
