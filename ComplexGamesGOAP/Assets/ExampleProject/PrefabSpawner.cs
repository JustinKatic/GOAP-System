using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    float timer = 0;
    public float spawnEveryX;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnEveryX)
        {
            SpawnPrefab();
            timer = 0;
        }
    }



    public void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, transform.position, transform.rotation);
    }
}

