using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Script made by Anton. Made for enemy to chase player using NavMeshAgent AI. (Isn't used ingame yet)

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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player died!");
        }
    }
}
