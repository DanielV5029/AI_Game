using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatternMovement : MonoBehaviour
{
    public GameObject[] pattern;
    private int patternIndex = 0;
    public float speed = 1;
    private float lastXPosition;
    private float lastYPosition;
    public GameObject enemy;
    private Animator animator;
    public heroMovement hm;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public HealthBar healthbar;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //lastXPosition = enemy.transform.position.x;
        //lastYPosition = enemy.transform.position.y;

        float deltaX = transform.position.x - lastXPosition;
        float deltaY = transform.position.y - lastYPosition;

        if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (transform.position.x > lastXPosition)
            {
                animator.SetBool("Left", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                animator.SetBool("Right", true);
                
            }
            else if (transform.position.x < lastXPosition)
            {
                animator.SetBool("Left", true);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                animator.SetBool("Right", false);
            }
        }
        else
        {
            if (transform.position.y > lastYPosition)
            {
                animator.SetBool("Left", false);
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
                animator.SetBool("Right", false);
            }
            else if (transform.position.y < lastYPosition)
            {
                animator.SetBool("Left", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);
                animator.SetBool("Right", false);
            }
        }

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

        lastXPosition = transform.position.x;
        lastYPosition = transform.position.y;

        transform.Translate(delta, Space.World);
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
    }
}
