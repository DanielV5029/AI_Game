using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorChase2 : MonoBehaviour
{
    public GUIStyle myStyle;
    public GameObject target;
    public float speed = 1.0f;
    public static int CurrentPowerUp = 1;
    public static int NewpowerUp = 0;
    public static int EnemiesAlive = 3;
    public float timeLeft = 5.0f;
    public bool yup = true;

    float timeToIncrease = 7.0f; //this is the time between "speedups"
    float currentTime;  //to keep track
    float speedIncrement = 0.4f; //how much to increase the speed by


    void Start()
    {
        currentTime = Time.time + timeToIncrease;
        transform.position = Random.insideUnitCircle * 12;
    }


    

    //ADDS POWER COUNT
    public void Apower()
    {
        CurrentPowerUp += 1;
    }

    //MINUS POWER COUNT
    public void Dpower()
    {
        CurrentPowerUp -= 1;
        yup = false;
    }

    //MINUS ENEMY COUNT
    public void Denemy()
    {
        EnemiesAlive -= 1;
    }

    //IF COLLIDES WITH HERO
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            if (yup == true)
            {
                Destroy(collision.gameObject);
                Application.LoadLevel(0);
                CurrentPowerUp = 1;
            }
            else if (yup == false)
            {
                Destroy(gameObject);
                Denemy();
            }


        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Time.time >= currentTime)
        {
            speed += speedIncrement;
            currentTime = Time.time + timeToIncrease;
        }


        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;

        //IF POWER UP IS 1 ENEMY CHASES PLAYER
        if (yup == true)
        {
            // Find the range to close vector
            Vector3 rangeToClose = playerPos - enemyPos;

            // Draw this vector at the position of the enemy
            Debug.DrawRay(enemyPos, rangeToClose, Color.cyan);

            //Get the range of the magnitude to the Player from Enemy
            float distanceToPlayer = rangeToClose.magnitude;

            //Get direction to the player
            Vector3 direction = rangeToClose.normalized;

            //Draws and Debug message the magnitude and the direction of the player from the enemy
            Debug.DrawRay(enemyPos, direction, Color.magenta);
            Debug.Log("RTC Magnitude:  " + distanceToPlayer + " Direction: " + direction);


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

        //IF POWERUP = 0 ENEMY RUNS AWAY FROM PLAYER
        else if (yup == false)
        {
            //TIMER STARTS GOING DOWN
            timeLeft -= Time.deltaTime;

            //IF TIMER IS MORE THAN 0 ENEMY RUNS AWAY
            if (timeLeft > 0)
            {
                // Find the range to close vector
                Vector3 rangeToClose = enemyPos - playerPos;

                // Draw this vector at the position of the enemy
                Debug.DrawRay(enemyPos, rangeToClose, Color.cyan);

                //Get the range of the magnitude to the Player from Enemy
                float distanceToPlayer = rangeToClose.magnitude;

                //Get direction to the player
                Vector3 direction = rangeToClose.normalized;

                //Draws and Debug message the magnitude and the direction of the player from the enemy
                Debug.DrawRay(enemyPos, direction, Color.magenta);
                Debug.Log("RTC Magnitude:  " + distanceToPlayer + " Direction: " + direction);


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

            //IF TIMER LESS THAN 0 ENEMY CHASES PLAYER
            else if (timeLeft < 0)
            {
                yup = true;
                timeLeft = 5.0f;
                // Find the range to close vector
                Vector3 rangeToClose = playerPos - enemyPos;

                // Draw this vector at the position of the enemy
                Debug.DrawRay(enemyPos, rangeToClose, Color.cyan);

                //Get the range of the magnitude to the Player from Enemy
                float distanceToPlayer = rangeToClose.magnitude;

                //Get direction to the player
                Vector3 direction = rangeToClose.normalized;

                //Draws and Debug message the magnitude and the direction of the player from the enemy
                Debug.DrawRay(enemyPos, direction, Color.magenta);
                Debug.Log("RTC Magnitude:  " + distanceToPlayer + " Direction: " + direction);


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
}

