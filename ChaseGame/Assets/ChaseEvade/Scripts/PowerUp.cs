using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject powerUpPrefab2;
    private Vector2 screenBounds;
    private Vector2 screenBounds2;

    public EnemyFiniteStates EFS1;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(spawnWave1());
        screenBounds2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(spawnWave2());
    }

    public void spawnPower1()
    {
        GameObject p = Instantiate(powerUpPrefab) as GameObject;
        p.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
        EFS1.Apower();
    }

    public void spawnPower2()
    {
        GameObject p1 = Instantiate(powerUpPrefab2) as GameObject;
        p1.transform.position = new Vector2(Random.Range(-screenBounds2.x, screenBounds2.x), Random.Range(-screenBounds2.y, screenBounds2.y));
        EFS1.Apower2();
    }

    IEnumerator spawnWave1()
    {
        while(true)
        {
            yield return new WaitForSeconds(15f);
            spawnPower1();
        }
    }

    IEnumerator spawnWave2()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            spawnPower2();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
