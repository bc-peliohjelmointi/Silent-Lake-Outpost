using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour // - Aleksi
{
    public GameObject footstepground;
    public GameObject footstepfloor;

    private bool isGroundTagged = false;
    private bool isFloorTagged = false;
    private bool isRunning = false;
    private bool isShiftPressed = false;

    void Start()
    {
        footstepground.SetActive(false);
        footstepfloor.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShiftPressed = true;
        }

        if (Input.GetKey("w") && isShiftPressed)
        {
            footsteps(true); 
        }
        else if (!Input.GetKey("w") || !isShiftPressed) 
        {
            footsteps(false);
        }

        if ((Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !isShiftPressed && !Input.GetKey("w"))
        {
            footsteps(false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isShiftPressed = false;
            footsteps(false); 
        }

        CheckGroundTags();
    }

    void footsteps(bool isRunning)
    {
        if (!isGroundTagged && !isFloorTagged)
        {
            return;
        }

        this.isRunning = isRunning;

        bool isMoving = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");

        if (isMoving)
        {
            if (isGroundTagged)
            {
                footstepground.SetActive(true);
                footstepfloor.SetActive(false);
                if (isRunning)
                {
                    footstepground.GetComponent<AudioSource>().pitch = 1.3f; // running speed ground
                }
                else
                {
                    footstepground.GetComponent<AudioSource>().pitch = 0.9f; // walking speed ground
                }
            }
            else if (isFloorTagged)
            {
                footstepfloor.SetActive(true);
                footstepground.SetActive(false);
                if (isRunning)
                {
                    footstepfloor.GetComponent<AudioSource>().pitch = 1.7f; // walking speed woodfloor
                }
                else
                {
                    footstepfloor.GetComponent<AudioSource>().pitch = 1.2f; // walking speed wood floor
                }
            }
        }
        else
        {
            footstepground.SetActive(false);
            footstepfloor.SetActive(false);
        }
    }

    void CheckGroundTags() // Change from wood to ground footstep
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isGroundTagged = true;
                isFloorTagged = false;
            }
            else if (hit.collider.CompareTag("WoodFloor"))
            {
                isGroundTagged = false;
                isFloorTagged = true;
            }
            else
            {
                isGroundTagged = false;
                isFloorTagged = false;
            }
        }
        else
        {
            isGroundTagged = false;
            isFloorTagged = false;
        }
    }
}
