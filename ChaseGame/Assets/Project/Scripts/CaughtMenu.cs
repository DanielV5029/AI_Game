using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaughtMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu"); //This gets the current scene and goes to the next available scene
    }
    public void Restart()
    {
        SceneManager.LoadScene("Level 1"); //This gets the current scene and goes to the next available scene
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
