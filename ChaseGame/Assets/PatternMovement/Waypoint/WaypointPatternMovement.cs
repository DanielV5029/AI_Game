using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatternMovement : MonoBehaviour
{
    public GameObject[] pattern;
    private int patternIndex = 0;
    public float speed = 1;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
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
}
