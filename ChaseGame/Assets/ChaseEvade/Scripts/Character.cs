using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float speed = 3.0f;
    public float time = 0f;
    public int points = 0;
    public GUIStyle myStyle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0f)
        {
            addScore();
            time = time - 0.5f;
        }
    }

    private void addScore()
    {
        points++;
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        MoveCharacter();
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 40, 160, 30), "Points: " + points, myStyle);
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


    public VectorChase Vchase;
    public VectorChase2 Vchase2;
    public VectorChase3 Vchase3;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "PowerUp")
        {
            Destroy(collision.gameObject);
            Vchase.Dpower();
            Vchase2.Dpower();
            Vchase3.Dpower();
        }
        if (collision.gameObject.name == "PowerUp(Clone)")
        {
            Destroy(collision.gameObject);
            Vchase.Dpower();
            Vchase2.Dpower();
            Vchase3.Dpower();

        }
    }
}