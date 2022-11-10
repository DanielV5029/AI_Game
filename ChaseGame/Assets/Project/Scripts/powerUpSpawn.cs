using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject powerUpPrefab;
    public enemyBehaviour eb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnWave1());
    }

    public void spawnPower1()
    {
        
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(powerUpPrefab, spawnPoints[randSpawnPoint].position, transform.rotation);
    }


    IEnumerator spawnWave1()
    {
        while (true)
        {
                yield return new WaitForSeconds(15f);
                spawnPower1();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
