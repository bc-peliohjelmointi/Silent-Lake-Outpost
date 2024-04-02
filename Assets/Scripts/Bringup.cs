using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringup : MonoBehaviour
{
    public GameObject setting;
    public bool issettingactive;
    public MouseLook mouseLookScript; // Reference to the MouseLook script

    [SerializeField] GameObject flashLight;
    [SerializeField] GameObject binoculars;

    public bool isPaused = false;


    void Start()
    {
        // Assign the MouseLook script reference
        mouseLookScript = GetComponent<MouseLook>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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

    public void Pause()
    {
        setting.SetActive(true);
        issettingactive = true;
        mouseLookScript.SetLookingEnabled(false); // Disable looking
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // Ensure cursor is visible when pause menu is active
        flashLight.SetActive(false);
        binoculars.SetActive(false);
        isPaused = true;
    }

    public void Resume()
    {
        setting.SetActive(false);
        issettingactive = false;
        mouseLookScript.SetLookingEnabled(true); // Enable looking
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Ensure cursor is hidden when pause menu is inactive
        isPaused = false;
    }
}
