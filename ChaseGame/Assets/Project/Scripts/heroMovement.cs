using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heroMovement : MonoBehaviour
{
    public float speed = 3.0f;
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;
    private int key = 3;
    public int maxHealth = 100;
    public int currentHealth = 100;
    private int points = 0;
    private Animator animator;
    public enemyBehaviour eb;
    public enemyBehaviour eb1;
    public enemyBehaviour eb2;
    public enemyBehaviour eb3;
    public enemyBehaviour eb4;
    public enemyBehaviour eb5;
    public enemyBehaviour eb6;
    public enemyBehaviour eb7;
    public exitChange ec;
    public HealthBar healthbar; 

    public GUIStyle myStyle;


    void OnGUI()
    {
        GUI.Box(new Rect(10, 20, 160, 30), "Points: " + points, myStyle);
        GUI.Box(new Rect(150, 20, 160, 30), "Keys Left: " + key, myStyle);
    }
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

        if(currentHealth == 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (key == 0)
        {
            ec.spriteChange();
        }
        MoveCharacter();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }

    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("left", false);
            animator.SetBool("up", true);
            animator.SetBool("down", false);
            animator.SetBool("right", false);
            left = false;
            up = true;
            down = false;
            right = false;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("left", true);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("right", false);
            left = false;
            up = true;
            down = false;
            right = false;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("left", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);
            animator.SetBool("right", false);
            left = false;
            up = false;
            down = true;
            right = false;
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("left", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("right", true);
            left = false;
            up = false;
            down = false;
            right = true;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "ExitDoor")
        {
            if (key == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void addScore()
    {
        points += 5;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "coin")
        {
            Debug.Log("Player hit: " + collision.name);
            Destroy(collision.gameObject);
            addScore();
        }
        if (collision.gameObject.name == "powerUp(Clone)")
        {
            Destroy(collision.gameObject);
            eb.Dpower();
            eb1.Dpower();
            eb2.Dpower();
            eb3.Dpower();
            eb4.Dpower();
            eb5.Dpower();
            eb6.Dpower();
            eb7.Dpower();
        }
        if (collision.gameObject.name == "key(Clone)")
        {
            Destroy(collision.gameObject);
            key -= 1;
        }
        if (collision.gameObject.name == "key1(Clone)")
        {
            Destroy(collision.gameObject);
            key -= 1;
        }
        if (collision.gameObject.name == "key2(Clone)")
        {
            Destroy(collision.gameObject);
            key -= 1;
        }
    }
}
