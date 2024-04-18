using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    
    private void Update()
    {
        agent.SetDestination(Player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player died!");
    }
}
