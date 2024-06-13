using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfInstances = 10;
    public float maxOffset = 1.0f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is not assigned!");
            return;
        }

        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * maxOffset;
            GameObject newObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
