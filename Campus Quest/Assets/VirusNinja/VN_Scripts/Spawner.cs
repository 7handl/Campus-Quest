using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] virusPrefabs;

    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 15f;
    public float maxForce = 18f;

    public float maxLifetime = 5f;


    private void Awake()
    {
        spawnArea = GetComponent<Collider>();

    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab = virusPrefabs[Random.Range(0, virusPrefabs.Length)];

            if(Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.z = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject virus = Instantiate(prefab, position, rotation);

            Destroy(virus, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            virus.GetComponent<Rigidbody>().AddForce(virus.transform.up * force, ForceMode.Impulse);


            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }

    }
}
