using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predictive : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;
    public float T = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Find the vector between the enemy and the player.
        // i.e. The range to close. If you add this vector 
        // to the enemy's position the enemy will be right 
        // on top of the player.

        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;

        SteeringPlayerMovement spm = target.GetComponent<SteeringPlayerMovement>();
        Vector3 playerVel = spm.getVelocity();

        Vector3 predictedPlayerPos = playerPos + playerVel.normalized * T;

        // Find the range to close vector
        Vector3 rangeToClose = predictedPlayerPos - enemyPos;

        // Draw this vector at the position of the enemy
        Debug.DrawRay(enemyPos, rangeToClose, Color.cyan);

        // Get the distance to the target
        float distance = rangeToClose.magnitude;

        // How far do we move each frame.
        // speedDelta is how much we want to move in 1 sec
        // We need to scale this down to how much we move in a frame
        // Time.deltaTime is the time elapsed since the last frame (typically 1/60s)
        float speedDelta = speed * Time.deltaTime;

        // Only move in the direction of the player if our distance
        // to the player is greater than how much we will move in the frame
        if (distance > speedDelta)
        {
            // Find our direction to the 
            Vector3 normalizedRangeToClose = rangeToClose.normalized;

            // Draw this vector at the position of the enemy
            Debug.DrawRay(enemyPos, normalizedRangeToClose, Color.green);

            // Multipling the speedDelta by the normalizedRangeToClose
            // will give us our displacement vector.
            Vector3 delta = speedDelta * normalizedRangeToClose;

            // Tranform our enemy in the direction of our player
            transform.Translate(delta);
        }
    }
}