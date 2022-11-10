using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitChange : MonoBehaviour
{
    public SpriteRenderer exitClosed;
    public Sprite exitOpened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spriteChange()
    {
        exitClosed.sprite = exitOpened;
    }
}

