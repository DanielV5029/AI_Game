using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RepeatControlData1
{
    public float moveX;
    public int repeat;
}

public class RepeatPatternMovement : MonoBehaviour {

    private RepeatControlData1[] pattern;
    private int patternIndex = 0;
    private int repeatIndex = 0;
    public float speed = 1;

    // Use this for initialization
    void Start ()
    {
        // Create some control data
        RepeatControlData1 pdr = new RepeatControlData1();
        pdr.moveX = 1.0f;
        pdr.repeat = 500;

        RepeatControlData1 pdl = new RepeatControlData1();
        pdl.moveX = -1.0f;
        pdl.repeat = 500;

        // Create a pattern (or an instruction list) with the control data
        pattern = new RepeatControlData1[] { pdr, pdl };
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Process the current instruction in our control data array
        RepeatControlData1 cd = pattern[patternIndex];
        float deltaX = cd.moveX * speed * Time.deltaTime;
        transform.position += new Vector3(deltaX, 0, 0);

        if (repeatIndex >= cd.repeat)
        {
            patternIndex++;
            repeatIndex = 0;
        }
        else
        {
            repeatIndex++;
        }

        // Reset the patternIndex if we are at the end of the instruction array
        if (patternIndex >= pattern.Length)
        {
            patternIndex = 0;
        }
    }
}