using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Script made by Anton. Made for enemy to chase player using NavMeshAgent AI. (Isn't used ingame yet)

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    private Animator animator;
    public float attackRange = 2f;

    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                agent.isStopped = true;
                animator.SetTrigger("Attack");
                isAttacking = true;
            }
            else
            {
                
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            if (isAttacking)
            {
                agent.isStopped = false;
                isAttacking = false;
            }

            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player died!");
        }
    }
}
