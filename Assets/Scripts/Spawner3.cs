using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3 : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Prefab of the object to be spawned")]
    public GameObject objectToSpawn;

    [Tooltip("Interval between spawns in seconds")]
    public float spawnInterval = 2.0f;

    [Tooltip("Total number of objects to spawn")]
    public int totalSpawnCount = 10;

    [Tooltip("Position where the objects will be spawned")]
    public Transform spawnPosition;

    private int currentSpawnCount = 0;

    private void Start()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Spawner3: No object assigned to spawn.");
            return;
        }

        if (spawnPosition == null)
        {
            Debug.LogError("Spawner3: No spawn position assigned. Defaulting to spawner's position.");
            spawnPosition = transform;
        }

        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (currentSpawnCount < totalSpawnCount)
        {
            SpawnObject();
            currentSpawnCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPosition.position, spawnPosition.rotation);
        float fps = 1.0f / Time.deltaTime;
        Debug.Log($"Spawned object #{currentSpawnCount + 1}. FPS at spawn: {fps}");
    }
}
