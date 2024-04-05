using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public GameObject footstepground;
    public GameObject footstepfloor;

    private bool isGroundTagged = false;
    private bool isFloorTagged = false;

    void Start()
    {
        footstepground.SetActive(false);
        footstepfloor.SetActive(false);
    }

    void Update()
    {
        bool isRunning = Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            footsteps(true);
        }
        else
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d"))
            {
                footsteps(false);
            }

            if (Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopFootsteps();
            }
        }

        CheckGroundTags();
    }

    void footsteps(bool isRunning)
    {
        if (isGroundTagged)
        {
            footstepground.SetActive(true);
            footstepfloor.SetActive(false);
            if (isRunning)
            {
                footstepground.GetComponent<AudioSource>().pitch = 1.5f;
            }
            else
            {
                footstepground.GetComponent<AudioSource>().pitch = 1f;
            }
        }
        else if (isFloorTagged)
        {
            footstepfloor.SetActive(true);
            footstepground.SetActive(false);
            if (isRunning)
            {
                footstepfloor.GetComponent<AudioSource>().pitch = 1.5f;
            }
            else
            {
                footstepfloor.GetComponent<AudioSource>().pitch = 1f;
            }
        }
    }

    void StopFootsteps()
    {
        footstepground.SetActive(false);
        footstepfloor.SetActive(false);
    }

    void CheckGroundTags()
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
