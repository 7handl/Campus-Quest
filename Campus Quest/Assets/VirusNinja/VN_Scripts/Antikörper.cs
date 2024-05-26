
using Unity.Collections;
using UnityEngine;

public class Antikörper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<VNGameManager>().Explode();
        }
    }
}
