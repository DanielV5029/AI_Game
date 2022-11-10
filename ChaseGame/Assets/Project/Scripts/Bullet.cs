using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 Velocity;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "CollisionMap")
        {
            Destroy(gameObject);
        }
    }
}
