using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    public GUIStyle myStyle;
    public GameObject target;
    public float speed = 1.0f;
    


    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;

        // Find the range to close vector
        Vector3 rangeToClose = playerPos - enemyPos;


        //Get the range of the magnitude to the Player from Enemy
        float distanceToPlayer = rangeToClose.magnitude;

        //Get direction to the player
        Vector3 direction = rangeToClose.normalized;

        
        Debug.Log("RTC Magnitude:  " + distanceToPlayer + " Direction: " + direction);

        if (distanceToPlayer < 10)
        {
            

            float speedDelta = speed * Time.deltaTime;
            //Makes sure there is no overshoot when the enemy is on top
            if (speedDelta < distanceToPlayer)
            {
                //How fast we want the enemy move per frame
                Vector3 delta = direction * speed * Time.deltaTime;

                //Move the enemy
                transform.Translate(delta);
            }
        }
    }
}

