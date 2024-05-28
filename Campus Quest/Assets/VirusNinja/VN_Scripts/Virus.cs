
using UnityEngine;

public class VirusBehave : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody virusRigidbody;
    private Collider virusCollider;
    private ParticleSystem juiceParticleEffect;

    private void Awake()
    {
        virusRigidbody = GetComponent<Rigidbody>();
        virusCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<VNGameManager>().IncreaseScore();

        whole.SetActive(false);
        sliced.SetActive(true);

        virusCollider.enabled = false;
        juiceParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = virusRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
