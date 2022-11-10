using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersManager : MonoBehaviour
{
    public GameObject spaceInvader;
    public Transform startTransform;

    // Use this for initialization
    void Start ()
    {
        //GameObject invaderClone = Instantiate(spaceInvader, transform.position, transform.rotation);
        // Instantiate space invaders on the screens
        int i = 0;
        for (i = 0; i < 5; i++)
        {

            for (int j = 0; j < 10; j++)
            {
                Vector3 gap = new Vector3(j, -i, 0);
                Instantiate(spaceInvader, startTransform.position + gap, transform.rotation);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
