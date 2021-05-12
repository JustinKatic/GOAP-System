using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;

    public void SpawnObject()
    {
        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
