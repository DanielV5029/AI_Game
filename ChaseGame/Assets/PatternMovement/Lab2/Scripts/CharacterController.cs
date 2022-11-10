using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public int speed = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() //Everyone runs this at 60 fps
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * 4 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Enemy")
        {
            Destroy(gameObject);
            Application.LoadLevel(1);
        }
        if (collision.gameObject.name == "SpikeBall")
        {
            Destroy(gameObject);
            Application.LoadLevel(1);
        }
        if (collision.gameObject.name == "ExitDoor")
        {
            if(star == 16)
            {
                Application.LoadLevel(2);
            }
        }
    }

    public GUIStyle myStyle;
    public int star = 0;
    void OnGUI()
    {
        GUI.Box(new Rect(10, 20, 160, 30), "Stars Collected: " + star, myStyle);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player hit: " + collision.name);
        star += 1;
        Destroy(collision.gameObject);
    }
}
