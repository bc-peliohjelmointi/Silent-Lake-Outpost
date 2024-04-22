using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringup : MonoBehaviour // - Aleksi
{
    public GameObject setting;
    public bool issettingactive;

    [SerializeField] GameObject flashLight;
    [SerializeField] GameObject binoculars;

    public bool isPaused = false;


    void Start()
    {
        
    }

    void Update() // Esc to activate pausemenu
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (issettingactive == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause() //Pause menu active
    {
        setting.SetActive(true);
        issettingactive = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        flashLight.SetActive(false);
        binoculars.SetActive(false);
        isPaused = true;
    }

    public void Resume() //  Resume game
    {
        setting.SetActive(false);
        issettingactive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        isPaused = false;
    }
}
