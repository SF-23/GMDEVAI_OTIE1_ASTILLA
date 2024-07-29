using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator animator;

    float speedMultiplier;
    float detectionRadius = 20f;
    float fleeRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        animator.SetFloat("wOffset", Random.Range(0.1f, 1f));

        ResetAgent();
         
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(agent.remainingDistance < 1f)
        {
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    void ResetAgent()
    {
        speedMultiplier = Random.Range(0.1f, 1f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        animator.SetFloat("speedMultiplier", speedMultiplier);
        animator.SetTrigger("isWalking");
        agent.ResetPath();
    }

    public void DetectMonster(Vector3 location)
    {
        if(Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - location).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if(path.status != NavMeshPathStatus.PathInvalid) 
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    public void DetectWaypoint(Vector3 location)
    {
        if(Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            agent.SetDestination(location);
            animator.SetTrigger("isRunning");
            agent.speed = 10;
            agent.angularSpeed = 500;
        }
    }
}
