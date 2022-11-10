using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 3.0f;
    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }


    [System.Obsolete]
    void FixedUpdate()
    {
        MoveCharacter();
    }


    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }


    public EnemyFiniteStates EFS1;
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "PowerUp")
        {
            Destroy(collision.gameObject);
            EFS1.Dpower();
        }
        if (collision.gameObject.name == "PowerUp(Clone)")
        {
            Destroy(collision.gameObject);
            EFS1.Dpower();
        }
        if (collision.gameObject.name == "PowerUp2")
        {
            Destroy(collision.gameObject);
            EFS1.Dpower2();
        }
        if (collision.gameObject.name == "PowerUp2(Clone)")
        {
            Destroy(collision.gameObject);
            EFS1.Dpower2();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        EFS1.addScore();
    }
}