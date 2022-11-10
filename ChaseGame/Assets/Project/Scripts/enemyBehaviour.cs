using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public GUIStyle myStyle;
    public GameObject target;
    public float speed = 1.0f;
    private static int NewpowerUp1 = 0;
    public float timeLeft = 5.0f;
    public bool powerUp1 = true;
    public GameObject[] pattern;
    private int patternIndex = 0;
    private float lastXPosition;
    private float lastYPosition;
    public GameObject enemy;
    private Animator animator;
    public int maxHealth = 100;
    public int currentHealth = 0;
    public HealthBar healthbar;




    float timeToIncrease = 7.0f; //this is the time between "speedups"
    float currentTime;  //to keep track

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
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    //MINUS POWER COUNT
    public void Dpower()
    {
        powerUp1 = false;
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
        if (distanceToPlayer < 6)
        {
            nearPlayer = true;
        }
        else
        {
            nearPlayer = false;
        }
    }

    public heroMovement hm;

    //IF COLLIDES WITH HERO
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            if (powerUp1 == true)
            {
                hm.TakeDamage(20);
            }
        }
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Destroy(collision.gameObject);
            TakeDamage(20);
            if (currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckIfNearPlayer();

        // What state are we in?
        switch (state)
        {
            case enemyStates.PATROL:
                Patroling();
                if (nearPlayer == true)
                {
                    initChasing();
                }
                else if (powerUp1 == false && nearPlayer == true)
                {
                    initRunAway();
                }
                break;

            case enemyStates.ATTACK:
                Chasing();
                if (nearPlayer == false)
                {
                    initPatrolling();
                }
                else if (powerUp1 == false)
                {
                    initRunAway();
                }
                break;

            case enemyStates.RUN:
                timeLeft -= Time.deltaTime;
                if (timeLeft > 0 && nearPlayer == true)
                {
                    runAway();
                }
                else if (timeLeft < 0)
                {
                    powerUp1 = true;
                    timeLeft = 5;
                    if (nearPlayer == true && powerUp1 == true)
                    {
                        initChasing();
                    }
                    else if (nearPlayer == false)
                    {
                        initPatrolling();
                    }
                }
                else if (nearPlayer == false)
                {
                    powerUp1 = true;
                    timeLeft = 5;

                    initPatrolling();
                }
                break;
        }

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

        transform.Translate(delta, Space.World);
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

