using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bullet;
    public bool willFire = true;
    Vector3 shootDirection;
    private float timeLeft = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            if(willFire == true)
            {
                Shoot(15);
                willFire = false;
            }
        }
        else
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0.5f;
                willFire = true;
            }

        }
    }

    public void Shoot(float speed)
    {
        // Get the position of the mouse on the screen.
        Vector3 screenMousePos = Input.mousePosition;

        // Turn that screen position into the actual position in the world.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        // Find out the direction between the player and the mouse pointer.
        Vector2 direction = mousePos - (Vector2)transform.position;

        // Normalize the direction and multiply by bullet speed.
        direction.Normalize();
        direction *= speed;

        // Spawn the bullet from the prefab.
        GameObject Bullets = Instantiate(bullet, transform.position, Quaternion.identity);

        // Find the BulletScript prefab on that spawned bullet, and set it's velocity component.
        Bullets.GetComponent<Bullet>().Velocity = direction;
    }
}
