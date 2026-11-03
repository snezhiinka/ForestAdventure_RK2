using UnityEngine;

// Lessons 2 and 6: InvokeRepeating + Random, one Hazard[] array.
public class Spawner : MonoBehaviour
{
    public Hazard[] prefabs;
    public float spawnInterval = 2f;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 6f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, spawnInterval);
    }

    void Spawn()
    {
        if (prefabs == null || prefabs.Length == 0) return;
        Hazard pick = prefabs[Random.Range(0, prefabs.Length)];
        if (pick == null) return;

        Instantiate(pick,
            new Vector3(Random.Range(minX, maxX), spawnY, 0f),
            Quaternion.identity);
    }
}
