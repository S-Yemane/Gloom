using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target; //?
    [SerializeField] float chaseRange = 5f;


    NavMeshAgent navMeshAgent; //? need to search up
    float distanceToTarget = Mathf.Infinity; //
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); //??
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position); // measure distance between us and target(player)

        if (isProvoked)
        {
            EngageTarget();
        }else if(distanceToTarget <= chaseRange) // if the distance is less than 5f
        {
            isProvoked = true;
        }

    }

    private void EngageTarget()
    {
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);  // set the AI's destination to where our target(player) is (so that its following the player)
    }

    private void AttackTarget()
    {
        Debug.Log(name + " has seeked and is destyoying " + target.name);
    }

    private void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange); // (mid point, radius)
    }
}
