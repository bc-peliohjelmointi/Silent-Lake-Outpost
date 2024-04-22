using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script made by Anton. With this script the running sound moves and starts playing that the player hears after being woken up by a knock.

public class RunDownStairs : MonoBehaviour
{
    public Transform targetPoint1; 
    public Transform targetPoint2; 

    public float speed = 5f; 

    private Transform currentTarget;

    void Start()
    {
        currentTarget = targetPoint1; 
    }

    //Here the empty game object with the running sound starts moving towards point 1.

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == targetPoint1) ? targetPoint2 : targetPoint1;
        }
    }

    // Here the empty game object with the running sound starts moving towards point 2 after colliding with point 1.

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetPoint2)
        {
            Destroy(gameObject);
        }
        else if (other.transform == targetPoint1)
        {
            currentTarget = targetPoint2;
        }
    }
}
