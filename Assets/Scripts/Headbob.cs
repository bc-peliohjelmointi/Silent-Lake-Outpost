using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            StartBobbing();
        }



        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopBobbing();
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            StopBobbing();
        }


    }

    void StartBobbing()
    {
        Camera.GetComponent<Animator>().Play("HeadBobbing");
    }

    void StopBobbing()
    {
        Camera.GetComponent<Animator>().Play("New State");
    }
}