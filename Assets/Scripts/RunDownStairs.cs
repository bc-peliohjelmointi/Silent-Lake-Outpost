using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDownStairs : MonoBehaviour
{
    public Transform targetPoint1; // Reference to the first target point
    public Transform targetPoint2; // Reference to the second target point

    public float speed = 5f; // Speed of movement

    private Transform currentTarget; // The current target point

    void Start()
    {
        currentTarget = targetPoint1; // Start moving towards targetPoint1
    }

    void Update()
    {
        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Check if we have reached the current target
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // If the current target is targetPoint1, switch to targetPoint2, otherwise switch to targetPoint1
            currentTarget = (currentTarget == targetPoint1) ? targetPoint2 : targetPoint1;
        }
    }

    // Collision detection
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the target point
        if (other.transform == targetPoint2)
        {
            // Destroy the RunningSound GameObject
            Destroy(gameObject);
        }
        else if (other.transform == targetPoint1)
        {
            // If collided with targetPoint1, switch to targetPoint2
            currentTarget = targetPoint2;
        }
    }
}
