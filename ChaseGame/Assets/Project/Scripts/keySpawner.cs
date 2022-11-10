using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keySpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] keyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        keySpawn1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void keySpawn1()
    {
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(keyPrefabs[0], spawnPoints[randSpawnPoint].position, transform.rotation);
    }
}
