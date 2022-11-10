using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiniteStates : MonoBehaviour
{
    public GUIStyle myStyle;
    public GameObject target;
    public float speed = 1.0f;
    public static int CurrentPowerUp1 = 1;
    public static int CurrentPowerUp2 = 1;
    public static int NewpowerUp1 = 0;
    public static int NewpowerUp2 = 0;
    public float timeLeft = 5.0f;
    public float timeLeftforSpeed = 5.0f;
    public bool powerUp1 = true;
    public bool powerUp2 = true;
    public bool speedUp = false;
    public int points = 0;
    public GameObject[] pattern;
    private int patternIndex = 0;

    float timeToIncrease = 7.0f; //this is the time between "speedups"
    float currentTime;  //to keep track
    float speedIncrement = 0.4f; //how much to increase the speed by


    public enum enemyStates
    {
        PATROL,
        ATTACK,
        RUN
    };

    public enemyStates state;

    void Start()
    {
        currentTime = Time.time + timeToIncrease;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 20, 160, 30), "Flee Power Ups: " + CurrentPowerUp1, myStyle);
        GUI.Box(new Rect(10, 40, 160, 30), "Slow Speed Power Ups: " + CurrentPowerUp2, myStyle);
        GUI.Box(new Rect(10, 60, 160, 30), "Points: " + points, myStyle);
    }


    //ADDS POWER COUNT
    public void Apower()
    {
        CurrentPowerUp1 += 1;
    }
    public void Apower2()
    {
        CurrentPowerUp2 += 1;
    }

    //MINUS POWER COUNT
    public void Dpower()
    {
        CurrentPowerUp1 -= 1;
        powerUp1 = false;
    }

    public void Dpower2()
    {
        CurrentPowerUp2 -= 1;
        powerUp2 = false;
    }

    public bool nearPlayer = false;

    public void CheckIfNearPlayer()
    {
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;

        // Find the range to close vector
        Vector3 rangeToClose = playerPos - enemyPos;
        //Get the range of the magnitude to the Player from Enemy
        float distanceToPlayer = rangeToClose.magnitude;

        //Get direction to the player
        Vector3 direction = rangeToClose.normalized;

        Debug.Log("DOP: " + distanceToPlayer);
        if (distanceToPlayer < 10)
        {
            nearPlayer = true;
        }
        else
        {
            nearPlayer = false;
        }
    }

    public void CheckIfSpeedUp()
    {
        if(points % 20 == 0 && points != 0)
        {
            speedUp = true;
        }
        else
        {
            speedUp = false;
        }
    }

    //IF COLLIDES WITH HERO
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            if (powerUp1 == true)
            {
                Destroy(collision.gameObject);
                Application.LoadLevel(0);
                CurrentPowerUp1 = 1;
            }
            else if (powerUp1 == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfNearPlayer();
        CheckIfSpeedUp();

        // What state are we in?
        switch (state)
        {
            case enemyStates.PATROL:
                Patroling();
                if (points > 29 && nearPlayer == true)
                {
                    initChasing();
                }
                else if (powerUp1 == false && nearPlayer == true)
                {
                    initRunAway();
                }
                
                else if (powerUp2 == false && speed > 0)
                {
                    speedDec();
                }

                else if(speedUp == true)
                {
                    speedInc();
                }
                break;

            case enemyStates.ATTACK:
                Chasing();
                if(nearPlayer == false)
                {
                    initPatrolling();
                }
                else if (nearPlayer == true && powerUp1 == false)
                {
                    initRunAway();
                }
                else if (speedUp == true)
                {
                    speedInc();
                }
                else if (powerUp2 == false && speed > 0)
                {
                    speedDec();
                }
                break;

            case enemyStates.RUN:
                timeLeft -= Time.deltaTime;
                if (timeLeft > 0 && nearPlayer == true)
                {
                    runAway();
                }
                else if(timeLeft < 0)
                {
                    powerUp1 = true;
                    timeLeft = 5;
                    if (points > 29 && nearPlayer == true && powerUp1 == true)
                    {
                        initChasing();
                    }
                    else if (nearPlayer == false)
                    {
                        initPatrolling();
                    }
                }
                else if(nearPlayer == false)
                {
                    powerUp1 = true;
                    timeLeft = 5;

                    initPatrolling();
                }
                else if (speedUp == true)
                {
                    speedInc();
                }
                else if (powerUp2 == false && speed > 0)
                {
                    speedDec();
                }
                break;
        }
    }

    public void addScore()
    {
        points += 5;
    }

    public void speedInc()
    {
        timeLeftforSpeed -= Time.deltaTime;
        if(timeLeft == 0)
        {
            speed += 1;
            timeLeftforSpeed = 5;
        }
    }

    
    public void speedDec()
    {
        
        speed -= 1;
        powerUp2 = true;
    }

    public void Patroling()
    {
        // Process the current instruction in our control data array
        GameObject waypoint = pattern[patternIndex];

        // Find the range to close vector
        Vector3 rangeToClose = waypoint.transform.position - transform.position;

        // What's our distance to the waypoint?
        float distance = rangeToClose.magnitude;

        // How far do we move each frame
        float speedDelta = speed * Time.deltaTime;

        // If we're close enough to the current waypoint 
        // then increase the pattern index

        if (distance <= speedDelta)
        {
            patternIndex++;
            // Reset the patternIndex if we are at the end of the instruction array
            if (patternIndex >= pattern.Length)
            {
                patternIndex = 0;
            }

            // Process the current instruction in our control data array
            waypoint = pattern[patternIndex];

            // Find the new range to close vector
            rangeToClose = waypoint.transform.position - transform.position;
        }

        // In what direction is our player?
        Vector3 normalizedRangeToClose = rangeToClose.normalized;

        Vector3 delta = speedDelta * normalizedRangeToClose;

        transform.Translate(delta);
    }

    public void initPatrolling()
    {
        Debug.Log("Starting to Patrol");
        state = enemyStates.PATROL;
    }

    public void Chasing()
    {
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;

        // Find the range to close vector
        Vector3 rangeToClose = playerPos - enemyPos;
        //Get the range of the magnitude to the Player from Enemy
        float distanceToPlayer = rangeToClose.magnitude;

        //Get direction to the player
        Vector3 direction = rangeToClose.normalized;
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

    public void initChasing()
    {
        Debug.Log("Starting to Chase");
        state = enemyStates.ATTACK;
    }

    public void runAway()
    {
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;
        // Find the range to close vector
        Vector3 rangeToClose = enemyPos - playerPos;

        //Get the range of the magnitude to the Player from Enemy
        float distanceToPlayer = rangeToClose.magnitude;

        //Get direction to the player
        Vector3 direction = rangeToClose.normalized;

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

    public void initRunAway()
    {
        Debug.Log("Starting to Chase");
        state = enemyStates.RUN;
    }
}

