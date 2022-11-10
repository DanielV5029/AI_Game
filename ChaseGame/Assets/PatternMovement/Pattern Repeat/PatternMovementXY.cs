using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDataXY
{
    public float moveX;
    public float moveY;
}

public class PatternMovementXY : MonoBehaviour
{

    private ControlDataXY[] pattern;
    private int patternIndex = 0;
    public float speed = 1;

    // Use this for initialization
    void Start()
    {
        // Create some control data
        ControlDataXY ru = new ControlDataXY();
        ru.moveX = 1.0f;
        ru.moveY = 1.0f;

        ControlDataXY rd = new ControlDataXY();
        rd.moveX = 1.0f;
        rd.moveY = -1.0f;

        ControlDataXY lu = new ControlDataXY();
        lu.moveX = -1.0f;
        lu.moveY = 1.0f;

        ControlDataXY ld = new ControlDataXY();
        ld.moveX = -1.0f;
        ld.moveY = -1.0f;

        // Create a pattern (or an instruction list) with the control data
        pattern = new ControlDataXY[] {
            ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru,
            rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd,
            ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru,
            rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd,
            ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru, ru,
            rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd, rd,
            lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu,
            ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld,
            lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu,
            ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld,
            lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu, lu,
            ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld, ld};

    }

    // Update is called once per frame
    void Update()
    {
        // Process the current instruction in our control data array
        ControlDataXY cd = pattern[patternIndex];
        float deltaX = cd.moveX * speed * Time.deltaTime;
        float deltaY = cd.moveY * speed * Time.deltaTime;
        transform.position += new Vector3(deltaX, deltaY, 0);

        // Increment the patternIndex so that we move to the next piece of pattern data
        patternIndex++;

        // Reset the patternIndex if we are at the end of the instruction array
        if (patternIndex >= pattern.Length)
        {
            patternIndex = 0;
        }

    }
}