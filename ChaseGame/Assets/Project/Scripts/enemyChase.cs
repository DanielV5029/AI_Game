using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChase : MonoBehaviour
{

    public GameObject target;
    public bool nearPlayer = false;
    public float timeLeft = 5.0f;
    public float speed = 1.0f;
    public GameObject enemy;
    private Animator animator;
    public heroMovement hm;
    public int maxHealth = 100;
    public int currentHealth = 0;
    public HealthBar healthbar;
    public GameObject[] pattern;
    private int patternIndex = 0;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfNearPlayer();

        if (nearPlayer == true)
        {

            Chasing();
        }
        else
        {
            backToSpawn();
        }

    }

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


    void backToSpawn()
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
                hm.TakeDamage(20);
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
}
