using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public int rows = 5;         
    public int columns = 5;         
    public float distance = 2f;    
    public float spawnInterval = 1f;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(j * distance, 0, i * distance);

                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
